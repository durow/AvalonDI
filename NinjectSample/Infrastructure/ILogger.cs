using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjectSample.Infrastructure
{
    public interface ILogger
    {
        void ToConsole(string text);
        void ClearList();
    }
}
