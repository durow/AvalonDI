using System;

namespace Ayx.AvalonDI
{
    public class DefaultContainer:IContainer
    {
        private static DefaultContainer _default;
        public static DefaultContainer Default
        {
            get
            {
                if (_default == null)
                    _default = new DefaultContainer(AyxContainer.Default);
                return _default;
            }
        }

        public AyxContainer AyxContainer { get; }

        public DefaultContainer(AyxContainer container)
        {
            if (container == null)
                throw new NullReferenceException("container can't be null");

            AyxContainer = container;
        }

        public object Get(Type type, string token = "")
        {
            return AyxContainer.Get(type, token);
        }

        public void Wire<T>(string token = "") where T:class
        {
            AyxContainer.Wire<T>(token);
        }
    }
}
