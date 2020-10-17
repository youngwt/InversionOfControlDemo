using System;
using System.Collections.Generic;
using System.Text;

namespace InversionOfControlDemo
{
    public class ContainerRegistration : IContainerRegistration
    {

        static Dictionary<Type, object> _registeredTypes = new Dictionary<Type, object>();


        public void AddSingleton<TService>()
        {
            throw new NotImplementedException();
        }

        public void AddSingleton<TService>(TService instance)
        {
            throw new NotImplementedException();
        }

        public void AddSingleton<TService, TConcrete>() where TConcrete : TService
        {
            throw new NotImplementedException();
        }

        public void AddSingleton<TService>(Func<IContainerRuntime, TService> factoryMethod)
        {
            throw new NotImplementedException();
        }

        public void AddTransient<TService>()
        {
            throw new NotImplementedException();
        }

        public void AddTransient<TService>(Func<IContainerRuntime, TService> factoryMethod)
        {
            throw new NotImplementedException();
        }

        public void AddTransient<TService, TConcrete>() where TConcrete : TService
        {
            throw new NotImplementedException();
        }

        public void AddTransient<TService, TConcrete>(Func<IContainerRuntime, TConcrete> factoryMethod) where TConcrete : TService
        {
            throw new NotImplementedException();
        }

        public IContainerRuntime CreateRuntime()
        {
            return new ContainerRuntime();
        }
    }
}
