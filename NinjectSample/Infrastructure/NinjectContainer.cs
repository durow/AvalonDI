using Ayx.AvalonDI;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjectSample.Infrastructure
{
    public class NinjectContainer : IContainer
    {
        public StandardKernel Container { get; private set; }

        public NinjectContainer(StandardKernel container)
        {
            Container = container;
        }
        public object Get(Type type, string token = "")
        {
            return Container.Get(type);
        }

        public void Wire<T>(string token = "") where T : class
        {
            Container.Bind<T>().ToSelf();
        }
    }
}
