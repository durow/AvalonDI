using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ayx.AvalonDI
{
    public class ViewModelInfo
    {
        public Type View { get; private set; }
        public Type ViewModel { get; private set; }
        public string Token { get; private set; }

        public ViewModelInfo(Type view, Type vm, string token = "")
        {
            View = view;
            ViewModel = vm;
            Token = token;
        }
    }
}
