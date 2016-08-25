using Ayx.AvalonDI;
using System.Windows;
using WpfSample.Infrastructure;
using WpfSample.Repository;
using WpfSample.ViewModels;
using WpfSample.Views;

namespace WpfSample
{
    public partial class App : Application
    {
        //View和ViewModel依赖
        public static AvalonContainer VM;
        //使用自己写的简易容器，可用其它容器替换
        public static AyxContainer Container;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //初始化依赖
            InitDependency();
            //显示主窗口
            VM.GetView<MainWindow>()?.Show();
        }

        private void InitDependency()
        {
            //依赖服务配置
            Container = new AyxContainer();
            Container.Wire<ITestDataRepo, TestDataRepo>();
            Container.WireSingleton<ILogger, SimpleLogger>();

            //使用自带的容器创建View和ViewModel的容器
            VM = new AvalonContainer(new DefaultContainer(Container));
            //View和ViewModel依赖配置
            VM.WireVM<MainWindow, MainWindowViewModel>();
            VM.WireVM<TestOneView, TestOneViewModel>();
        }
    }
}
