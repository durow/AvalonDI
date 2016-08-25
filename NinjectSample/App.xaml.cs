using Ayx.AvalonDI;
using Ninject;
using NinjectSample.Infrastructure;
using NinjectSample.Repository;
using NinjectSample.ViewModels;
using NinjectSample.Views;
using System.Windows;

namespace NinjectSample
{
    public partial class App : Application
    {
        //View和ViewModel的容器
        public static AvalonContainer VM;
        //Ninject容器
        public static StandardKernel Ninject;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //初始化服务依赖
            InitDependency();
            //显示主窗口
            VM.GetView<MainWindow>()?.Show();
        }

        private void InitDependency()
        {
            //配置Ninject依赖
            Ninject = new StandardKernel();
            Ninject.Bind<ITestDataRepo>().To<TestDataRepo>().InSingletonScope();
            Ninject.Bind<ILogger>().To<SimpleLogger>().InSingletonScope();
            
            //使用Ninject容器创建View和ViewModel依赖容器
            VM = new AvalonContainer(new NinjectContainer(Ninject));
            //配置View和ViewModel依赖
            VM.WireVM<MainWindow, MainWindowViewModel>();
            VM.WireVM<TestOneView, TestOneViewModel>();
        }
    }
}
