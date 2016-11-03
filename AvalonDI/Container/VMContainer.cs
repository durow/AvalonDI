using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Ayx.AvalonDI.Container
{
    public class VMContainer:IVMContainer
    {
        public readonly List<ViewModelInfo> VMList = new List<ViewModelInfo>();

        public void WireVM<TView, TViewModel>(string token = "", Func<object> createFunc = null)
            where TView : FrameworkElement where TViewModel : class
        {
            CheckVMExist<TView>(token);

            VMList.Add(new ViewModelInfo(
                typeof(TView),
                typeof(TViewModel),
                token));
        }

        public Type GetVMType<TView>(string token = "")
        {
            var fromType = typeof(TView);
            var vmInfo = GetViewModelInfo(fromType, token);
            if (vmInfo == null)
                return null;

            return vmInfo.ViewModel;
        }

        public void CheckVMExist<Tfrom>(string token = "")
        {
            var type = typeof(Tfrom);
            var result = GetViewModelInfo(type, token);
            if (result != null)
                throw new Exception($"type \"{type.Name}\" with token \"{token}\" have been wired!");
        }

        private ViewModelInfo GetViewModelInfo(Type type, string token = "")
        {
            var result = VMList.Where(p => p.View == type);
            if (!string.IsNullOrEmpty(token))
            {
                result = result.Where(p => p.Token == token);
            }
            return result.FirstOrDefault();
        }

        
    }
}
