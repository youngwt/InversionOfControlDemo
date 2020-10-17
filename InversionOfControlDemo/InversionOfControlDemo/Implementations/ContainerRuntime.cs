using System;
using System.Collections.Generic;
using System.Text;

namespace InversionOfControlDemo
{
    public class ContainerRuntime : IContainerRuntime
    {
        public TService GetService<TService>()
        {
            return Activator.CreateInstance<TService>();
        }

        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }
    }
}
