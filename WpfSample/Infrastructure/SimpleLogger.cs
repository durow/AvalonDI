using Ayx.CSLibrary.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace WpfSample.Infrastructure
{
    public class SimpleLogger : ILogger 
    {
        public ObservableCollection<string> LogList { get; set; }

        public SimpleLogger()
        {
            LogList = new ObservableCollection<string>();
        }

        public void ToConsole(string text)
        {
            var log = $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] {text}";
            Console.WriteLine(log);
            LogList.Add(log);
        }

        public void ClearList()
        {
            LogList.Clear();
        }
    }
}
