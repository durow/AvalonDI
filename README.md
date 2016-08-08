# AvalonDI
A DI Container for WPF!

##Install
```  
PM>Install-Package Ayx.AvalonDI
```

##Wire/Bind dependency
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
##Inject from constructor
```c
public class TestOneViewModel:NotificationObject
{
    private ITestDataRepo _repo;
    private ILogger _logger;

    public TestOneViewModel(ITestDataRepo repo, ILogger logger)
    {
        _repo = repo;
        _logger = logger;
    }
}
```

##Inject by "AutoInject" attribute
```c
public class TestOneViewModel:NotificationObject
{
    [AutoInject]
    public ITestDataRepo Repo{get;set;}
    
    [AutoInject]
    public ILogger Logger { get; set; }
}
```
But I suggest use constructor.

##Get a view
```c
public class TestOneViewModel:NotificationObject
{
    var view = App.DI.GetView<TestOneView>();
}
```
The DataContext of view will set to instance of TestOneViewModel automatically.
And the dependency of TestOneViewModel will be injected automatically.
