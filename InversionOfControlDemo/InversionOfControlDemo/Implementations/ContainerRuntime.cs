using System;
using System.Collections.Generic;
using System.Text;

namespace InversionOfControlDemo
{
    public class ContainerRuntime : IContainerRuntime
    {

        // Prevent instantiation
        public ContainerRuntime(ContainerRegistration containerRegistration ) {
            _container = containerRegistration;
        }

        private ContainerRegistration _container { get; set; }

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
