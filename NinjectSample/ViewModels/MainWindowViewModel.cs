using Ayx.CSLibrary.MVVM;
using NinjectSample.Infrastructure;
using NinjectSample.Views;

namespace NinjectSample.ViewModels
{
    public class MainWindowViewModel:NotificationObject
    {
        public ILogger Logger { get; set; }

        public MainWindowViewModel(ILogger logger)
        {
            Logger = logger;
            Logger?.ToConsole("Application load complete!");
        }

        #region Commands

        private AyxCommand _CmdOpenTest1;

        /// <summary>
        /// Gets the CmdOpenTest1.
        /// </summary>
        public AyxCommand CmdOpenTest1
        {
            get
            {
                if (_CmdOpenTest1 == null)
                    _CmdOpenTest1 = new AyxCommand(
                    o =>
                    {
                        Logger?.ToConsole("Try to open test1 view!");
                        //显示窗口
                        App.VM.GetView<TestOneView>()?.ShowDialog();
                        Logger?.ToConsole("Test1 view closed!");
                    });
                return _CmdOpenTest1;
            }
        }

        private AyxCommand _CmdOpenTest2;

        /// <summary>
        /// Gets the CmdOpenText2.
        /// </summary>
        public AyxCommand CmdOpenTest2
        {
            get
            {
                if (_CmdOpenTest2 == null)
                    _CmdOpenTest2 = new AyxCommand(
                    o =>
                    {
                        Logger?.ToConsole("Open test2 view!");
                    });
                return _CmdOpenTest2;
            }
        }

        private AyxCommand _CmdClearLog;

        /// <summary>
        /// Gets the CmdClearLog.
        /// </summary>
        public AyxCommand CmdClearLog
        {
            get
            {
                if (_CmdClearLog == null)
                    _CmdClearLog = new AyxCommand(
                    o =>
                    {
                        Logger?.ClearList();
                    });
                return _CmdClearLog;
            }
        }

        #endregion
    }
}
