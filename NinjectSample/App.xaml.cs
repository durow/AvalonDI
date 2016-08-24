using Ayx.AvalonDI;
using Ninject;
using NinjectSample.Infrastructure;
using NinjectSample.Repository;
using NinjectSample.ViewModels;
using NinjectSample.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace NinjectSample
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static AvalonContainer VM;
        public static StandardKernel DI;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            InitDependency();
            //show start window
            VM.GetView<MainWindow>().Show();
        }

        private void InitDependency()
        {
            DI = new StandardKernel();
            DI.Bind<ITestDataRepo>().To<TestDataRepo>().InSingletonScope();
            DI.Bind<ILogger>().To<SimpleLogger>().InSingletonScope();

            VM = new AvalonContainer(new NinjectContainer(DI));
            VM.WireVM<MainWindow, MainWindowViewModel>();
            VM.WireVM<TestOneView, TestOneViewModel>();
        }
    }
}
