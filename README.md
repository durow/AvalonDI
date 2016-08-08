# AvalonDI
A DI Container for WPF!

##Install
```  
PM>Install-Package Ayx.AvalonDI
```

##How to use
```C
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
```

