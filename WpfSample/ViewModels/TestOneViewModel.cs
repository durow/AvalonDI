using Ayx.CSLibrary.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfSample.Infrastructure;
using WpfSample.Models;
using WpfSample.Repository;

namespace WpfSample.ViewModels
{
    public class TestOneViewModel:NotificationObject
    {
        private ITestDataRepo _repo;
        private ILogger _logger;
        private ObservableCollection<TestData> _DataList;
        private TestData _SelectedData;

        public TestOneViewModel(ITestDataRepo repo, ILogger logger)
        {
            _repo = repo;
            _logger = logger;
            _logger?.ToConsole("Test1 view opened!");
        }

        public TestData SelectedData
        {
            get { return _SelectedData; }
            set
            {
                if (_SelectedData != value)
                {
                    _SelectedData = value;
                    NotifyPropertyChanged("SelectedData");
                }
            }
        }

        public ObservableCollection<TestData> DataList
        {
            get { return _DataList; }
            set
            {
                if (_DataList != value)
                {
                    _DataList = value;
                    NotifyPropertyChanged("DataList");
                }
            }
        }

        #region Commands

        private AyxCommand _CmdGet;
        /// <summary>
        /// Gets the CmdGet.
        /// </summary>
        public AyxCommand CmdGet
        {
            get
            {
                if (_CmdGet == null)
                    _CmdGet = new AyxCommand(
                    o =>
                    {
                        var data = _repo.Get(200);
                        DataList = new ObservableCollection<TestData>(data);
                        _logger?.ToConsole($"Get data count:{DataList.Count}");
                    });
                return _CmdGet;
            }
        }

        private AyxCommand _CmdDelete;
        /// <summary>
        /// Gets the CmdDelete.
        /// </summary>
        public AyxCommand CmdDelete
        {
            get
            {
                if (_CmdDelete == null)
                    _CmdDelete = new AyxCommand(
                    o =>
                    {
                        _repo.Delete(SelectedData);
                        var id = SelectedData.Id;
                        DataList.Remove(SelectedData);
                        _logger?.ToConsole($"Removed item id:{id}");
                    },o=>SelectedData!=null);
                return _CmdDelete;
            }
        }

        private AyxCommand _CmdUpdate;
        /// <summary>
        /// Gets the CmdUpdate.
        /// </summary>
        public AyxCommand CmdUpdate
        {
            get
            {
                if (_CmdUpdate == null)
                    _CmdUpdate = new AyxCommand(
                    o =>
                    {
                        _repo.Update(SelectedData);
                        _logger.ToConsole($"Update item id:{SelectedData.Id}");
                    },o=>SelectedData!=null);
                return _CmdUpdate;
            }
        }

        #endregion
    }
}
