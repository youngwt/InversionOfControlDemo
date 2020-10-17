using System;
using System.Collections.Generic;
using System.Text;

namespace InversionOfControlDemo
{
    public class ContainerRegistration : IContainerRegistration
    {

        private static Dictionary<Type, object> _registeredSingletons = new Dictionary<Type, object>();


        public void AddSingleton<TService>()
        {
            throw new NotImplementedException();
        }

        public void AddSingleton<TService>(TService instance)
        {
            _registeredSingletons.Add(typeof(TService), instance);
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

        /// <summary>
        /// Returns the keys registerd in our internal dictionary. Added for validating the container
        /// </summary>
        /// <returns>A collection of keys in for the registration</returns>
        public Dictionary<Type, object>.KeyCollection GetRegisteredSingletonTypes()
        {
            return _registeredSingletons.Keys;
        }
    }
}
