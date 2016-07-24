/*
 * Author:durow
 * Date:2015.12.30
 */

using Ayx.CSLibrary.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace Ayx.AvalonDI
{
    public class DIContainer
    {
        private static DIContainer _default;
        public static DIContainer Default
        {
            get
            {
                if (_default == null)
                    _default = new DIContainer();
                return _default;
            }
        }

        public int Count { get { return injectInfoList.Count; } }

        private  List<InjectInfo> injectInfoList = new List<InjectInfo>();

        public  void Wire<Tfrom,Tto>(string token = "", Func<object> createFunc=null) where Tto:Tfrom
        {
            CheckExist<Tfrom>(token);

            injectInfoList.Add(
                new InjectInfo
                {
                    From = typeof(Tfrom),
                    To = typeof(Tto),
                    Token = token,
                    InjectType = InjectType.Normal,
                    CreateFunction = createFunc,
                });
        }

        public void Wire<T>(string token = "", Func<object> createFunc = null)
        {
            var type = typeof(T);
            if (type.IsAbstract || type.IsInterface)
                throw new Exception("can't wire interface or abstraction!");

            CheckExist<T>(token);

            Wire<T, T>(token, createFunc);
        }

        public  void WireSingleton<Tfrom,Tto>(string token = "", Func<object> createFunc = null) where Tto:Tfrom
        {
            CheckExist<Tfrom>(token);

            injectInfoList.Add(
                 new InjectInfo
                 {
                     From = typeof(Tfrom),
                     To = typeof(Tto),
                     InjectType = InjectType.Singleton,
                     Token = token,
                     CreateFunction = createFunc,
                 });
        }

        public void WireSingleton<T>(string token = "", Func<object> createFunc = null)
        {
            CheckExist<T>(token);

            WireSingleton<T, T>();
        }
        public void WireSingleton<Tfrom>(Tfrom instance, string token = "")
        {
            CheckExist<Tfrom>(token);

            injectInfoList.Add(
                 new InjectInfo
                 {
                     From = typeof(Tfrom),
                     To = instance.GetType(),
                     InjectType = InjectType.Singleton,
                     instance = instance,
                     Token = token,
                 });
        }

        public void WireVM<TView, TViewModel>(string token = "", Func<object> createFunc = null)
            where TView : FrameworkElement where TViewModel : INotifyPropertyChanged
        {
            CheckExist<TView>(token);

            injectInfoList.Add(
                 new InjectInfo
                 {
                     From = typeof(TView),
                     To = typeof(TViewModel),
                     InjectType = InjectType.ViewModel,
                     Token = token,
                     CreateFunction = createFunc,
                 });
        }

        public T Get<T>(string token = "")
        {
            return (T)Get(typeof(T), token);
        }

        public object Get(Type fromType, string token = "")
        {
            object result = null;
            var itemDI = GetInjectionInfo(fromType,token);
            if (itemDI == null)
                result = CreateInstance(fromType);
            else
                result = CreateInstance(itemDI);
            InjectPropertyDependency(result);

            return result;
        }

        public object GetVM<TView>(string token = "")
        {
            return Get(typeof(TView), token);
        }

        public T GetView<T>(string token="") where T : FrameworkElement
        {
            var result = Activator.CreateInstance<T>();
            result.DataContext = GetVM<T>(token);
            return result;
        }

        public void Remove<T>(string token = "")
        {
            var find = false;
            var type = typeof(T);
            for (int i = 0; i < injectInfoList.Count; i++)
            {
                var item = injectInfoList[i];
                if(CheckEqual(item,type,token))
                {
                    find = true;
                    injectInfoList.Remove(item);
                    break;
                }
            }
            if (find)
                Remove<T>(token);
            else
                return;
        }

        public void CheckExist<Tfrom>(string token = "")
        {
            var type = typeof(Tfrom);
            var result = GetInjectionInfo(type, token);
            if (result != null)
                throw new Exception($"type \"{type.Name}\" with token \"{token}\" have been wired!");
        }

        #region Private Methods

        private InjectInfo GetInjectionInfo(Type type, string token = "")
        {
            var result = injectInfoList.Where(p => p.From == type);
            if (!string.IsNullOrEmpty(token))
            {
                result = result.Where(p => p.Token == token);
            }
            return result.FirstOrDefault();
        }

        private bool CheckEqual(InjectInfo info, Type t, string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                if (info.From == t)
                    return true;
                else
                    return false;
            }
            if (info.From == t && info.Token == token)
                return true;
            else
                return false;
        }

        private object CreateInstance(InjectInfo info)
        {
            if (info.InjectType == InjectType.Singleton)
            {
                if (info.instance == null)
                {
                    if (info.CreateFunction != null)
                        info.instance = info.CreateFunction();
                    else
                        info.instance = CreateInstance(info.To);
                }
                return info.instance;
            }
            return CreateInstance(info.To);
        }

        private object CreateInstance(Type type)
        {
            if (!type.IsClass) return null;

            var param = GetConstructorParameters(type).ToArray();
            var result = Activator.CreateInstance(type,param);

            return result;
        }

        private IEnumerable<object> GetConstructorParameters(Type type)
        {
            var result = new List<object>();
            var constructor = GetConstructor(type);
            if (constructor == null) yield break;

            foreach (var param in constructor.GetParameters())
            {
                if (param.DefaultValue == null || param.DefaultValue.ToString() != "")
                {
                    yield return param.DefaultValue;
                    continue;
                }
                var t = param.ParameterType;
                if (t == typeof(string))
                {
                    yield return "";
                    continue;
                }

                if (t.IsValueType)
                {
                    yield return null;
                    continue;
                }

                if (t.IsAbstract || t.IsClass)
                {
                    yield return Get(t);
                    continue;
                }
                yield return null;
            }
        }

        private ConstructorInfo GetConstructor(Type type)
        {
            var constructorList = type.GetConstructors();
            if (constructorList.Count() == 0)
                return null;

            if (constructorList.Count() == 1)
                return constructorList[0];

            foreach (var constructor in constructorList)
            {
                if(constructor.GetCustomAttributes(typeof(AutoInjectAttribute),false).Any())
                {
                    return constructor;
                }
            }
            return constructorList.FirstOrDefault();
        }

        private void InjectPropertyDependency(object o)
        {
            foreach (var property in o.GetType().GetProperties())
            {
                if (property.PropertyType.IsValueType)
                    continue;
                var attr = AttributeHelper.GetAttribute<AutoInjectAttribute>(property);
                if (attr == null)
                    continue;
                if (property.GetValue(o, null) != null)
                    continue;

                var valueType = property.PropertyType;
                var propertyValue = Get(valueType, attr.Token);
                property.SetValue(o, propertyValue, null);
            }
        }

        #endregion
    }
}
