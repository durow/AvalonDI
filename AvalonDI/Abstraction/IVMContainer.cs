using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Ayx.AvalonDI
{
    public interface IVMContainer
    {
        void WireVM<TView, TViewModel>(string token = "", Func<object> createFunc = null)
            where TView : FrameworkElement where TViewModel : class;

        Type GetVMType<TView>(string token = "");
    }
}
