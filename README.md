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

##Use 3rd part DI container e.g Ninject
```C#
public partial class App : Application
    {
        //View and ViewModel container
        public static AvalonContainer VM;
        //Ninject container
        public static StandardKernel Ninject;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            InitDependency();
            VM.GetView<MainWindow>()?.Show();
        }

        private void InitDependency()
        {
            Ninject = new StandardKernel();
            Ninject.Bind<ITestDataRepo>().To<TestDataRepo>().InSingletonScope();
            Ninject.Bind<ILogger>().To<SimpleLogger>().InSingletonScope();
            
            VM = new AvalonContainer(new NinjectContainer(Ninject));
            VM.WireVM<MainWindow, MainWindowViewModel>();
            VM.WireVM<TestOneView, TestOneViewModel>();
        }
    }
```
See NinjectSample for detail.

##Use token to make a View and ViewModel unique
```C#
VM.Wire<ViewA,ViewModelA>("A");
VM.Wire<ViewA,ViewModelAA>("AA");

var viewA = VM.GetView<ViewA>("A");   //view with DataContext ViewModelA
var viewAA = VM.GetView<ViewA>("AA"); //view with DataContext ViewModelAA
```
