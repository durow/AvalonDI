/*
 * Author:durow
 * Date:2016.11.03
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Ayx.AvalonDI
{
    interface IServiceLocator
    {
        T GetService<T>(string token = "");
        object GetService(Type type, string token = "");
        TView GetView<TView>(string token = "") where TView : FrameworkElement;
        TView GetView<TView,TViewModel>()
            where TView : FrameworkElement where TViewModel : class;
    }
}
