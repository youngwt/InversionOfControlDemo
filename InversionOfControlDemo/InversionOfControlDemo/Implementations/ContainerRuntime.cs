using System;
using System.Collections.Generic;
using System.Text;

namespace InversionOfControlDemo
{
    public class ContainerRuntime : IContainerRuntime
    {
        public ContainerRuntime(ContainerRegistration containerRegistration ) {
            _container = containerRegistration;
        }

        private ContainerRegistration _container { get; set; }

        public TService GetService<TService>()
        {
            var type = typeof(TService);

            return (TService) GetService(type);
        }

        public object GetService(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }
    }
}
