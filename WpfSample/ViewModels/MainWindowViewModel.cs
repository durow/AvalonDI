using Ayx.CSLibrary.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfSample.Infrastructure;
using Ayx.AvalonDI;
using WpfSample.Views;

namespace WpfSample.ViewModels
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
                        var view = App.DI.GetView<TestOneView>();
                        view?.ShowDialog();
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
