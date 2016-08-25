/*
 * Author:durow
 * Date:2016.08.22
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Ayx.AvalonDI
{
    public class AvalonContainer
    {
        private static AvalonContainer _default;
        public static AvalonContainer Default
        {
            get
            {
                if (_default == null)
                    _default = new AvalonContainer(DefaultContainer.Default);
                return _default;
            }
        }

        IContainer Container { get; }
        public readonly List<ViewModelInfo> VMList = new List<ViewModelInfo>();

        public AvalonContainer(IContainer container)
        {
            if (container == null)
                throw new NullReferenceException("container can't be null!");
            Container = container;
        }

        public T Get<T>(string token = "")
        {
            return (T)Container.Get(typeof(T),token);
        }

        public object Get(Type type, string token)
        {
            return Container.Get(type, token);
        }

        public void WireVM<TView,TViewModel>(string token = "",Func<object> createFunc = null)
            where TView : FrameworkElement where TViewModel : class
        {
            CheckVMExist<TView>(token);

            VMList.Add(new ViewModelInfo(
                typeof(TView),
                typeof(TViewModel),
                token));
            Container.Wire<TViewModel>(token);
        }

        public object GetVM<TView>(string token = "")
        {
            var fromType = typeof(TView);
            var vmInfo = GetViewModelInfo(fromType, token);
            if (vmInfo == null)
                throw new Exception($"can't find ViewModel of {fromType}");

            return Get(vmInfo.ViewModel, token);
        }

        public T GetView<T>(string token = "") where T:FrameworkElement
        {
            var result = Activator.CreateInstance<T>();
            result.DataContext = GetVM<T>(token);
            return result;
        }

        public void Remove<T>(string token = "")
        {
            var find = false;
            var type = typeof(T);
            for (int i = 0; i < VMList.Count; i++)
            {
                var item = VMList[i];
                if (CheckEqual(item, type, token))
                {
                    find = true;
                    VMList.Remove(item);
                    break;
                }
            }
            if (find)
                Remove<T>(token);
            else
                return;
        }

        public void CheckVMExist<Tfrom>(string token = "")
        {
            var type = typeof(Tfrom);
            var result = GetViewModelInfo(type, token);
            if (result != null)
                throw new Exception($"type \"{type.Name}\" with token \"{token}\" have been wired!");
        }

        #region Private Methods

        private ViewModelInfo GetViewModelInfo(Type type, string token = "")
        {
            var result = VMList.Where(p => p.View == type);
            if (!string.IsNullOrEmpty(token))
            {
                result = result.Where(p => p.Token == token);
            }
            return result.FirstOrDefault();
        }

        private bool CheckEqual(ViewModelInfo info, Type t, string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                if (info.View == t)
                    return true;
                else
                    return false;
            }
            if (info.View == t && info.Token == token)
                return true;
            else
                return false;
        }

        #endregion
    }
}
