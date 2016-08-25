# AvalonDI
A simple DI Container for WPF!

ViewModel's dependencies can be injected from constructor or by "AutoInject" attribute,just like controllers in MVC.

##Install
```  
PM>Install-Package Ayx.AvalonDI
```

##Wire/Bind dependency
```C#
public partial class App : Application
{
    //DI Container
    public static AyxContainer Container = new AyxContainer();
    //View container
    public static AvalonContainer VM;

    private void Application_Startup(object sender, StartupEventArgs e)
    {
        WireDependency();
        //show start window
        DI.GetView<MainWindow>().Show();
    }

    private void WireDependency()
    {
        //dependency
        Container.Wire<ITestDataRepo, TestDataRepo>();
        Container.WireSingleton<ILogger, SimpleLogger>();

        //view and viewmodel
        VM = new AvalonContainer(new DefaultContainer(Container));
        VM.WireVM<MainWindow, MainWindowViewModel>();
        VM.WireVM<TestOneView, TestOneViewModel>();
    }
}
```
##Inject from constructor
```C#
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
```C#
public class TestOneViewModel:NotificationObject
{
    [AutoInject]
    public ITestDataRepo Repo{get;set;}
    
    [AutoInject]
    public ILogger Logger { get; set; }
}
```
But I suggest use constructor to inject.

##Get a view
```C#
var view = App.VM.GetView<TestOneView>();
```
The DataContext of view will set to instance of TestOneViewModel automatically.

And the dependency of TestOneViewModel will be injected automatically.

##Use 3rd part DI container eg.Ninject
```C#
```

##Use token to make a dependency unique
```C#
App.DI.Wire<IService,ServiceA>("A");
App.DI.Wire<IService,ServiceB>("B");

var serviceA = App.DI.Get<IService>("A");
var serviceB = App.DI.Get<IService>("B");
```
