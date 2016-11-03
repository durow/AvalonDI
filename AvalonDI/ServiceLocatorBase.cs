using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Ayx.AvalonDI
{
    public abstract class ServiceLocatorBase : IServiceLocator
    {
        private IVMContainer VMContainer;

        public void SetVMContainer(IVMContainer vmContainer)
        {
            VMContainer = vmContainer;
        }

        public abstract object GetService(Type type, string token = "");

        public abstract T GetService<T>(string token = "");

        public TView GetView<TView>(string token = "") where TView : FrameworkElement
        {
            var result = Activator.CreateInstance<TView>();
            if (VMContainer == null) return result;
            var vmType = VMContainer.GetVMType<TView>(token);
            result.DataContext = GetService(vmType);
            return result;
        }

        public TView GetView<TView, TViewModel>()
            where TView : FrameworkElement
            where TViewModel : class
        {
            var result = Activator.CreateInstance<TView>();
            result.DataContext = GetService<TViewModel>();
            return result;
        }
    }
}