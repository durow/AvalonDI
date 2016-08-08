using Ayx.AvalonDI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfSample.Infrastructure;
using WpfSample.Repository;
using WpfSample.ViewModels;
using WpfSample.Views;

namespace WpfSample
{
    public partial class App : Application
    {
        //DI container
        public static DIContainer DI = new DIContainer();

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            WireDependency();
            //show start window
            DI.GetView<MainWindow>().Show();
        }

        private void WireDependency()
        {
            //dependency
            DI.Wire<ITestDataRepo, TestDataRepo>();
            DI.WireSingleton<ILogger, SimpleLogger>();

            //view and viewmodel
            DI.WireVM<MainWindow, MainWindowViewModel>();
            DI.WireVM<TestOneView, TestOneViewModel>();
        }
    }
}
