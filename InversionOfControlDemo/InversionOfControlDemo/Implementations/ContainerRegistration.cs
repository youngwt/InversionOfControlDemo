using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InversionOfControlDemo
{
    public class ContainerRegistration : IContainerRegistration
    {

        private static Dictionary<Type, Func<object>> _registeredSingletons;

        public ContainerRegistration()
        {
            _registeredSingletons = new Dictionary<Type, Func<object>>();
        }

        public void AddSingleton<TService>()
        {

            //var types =
            //        from a in AppDomain.CurrentDomain.GetAssemblies()
            //        from t in a.GetTypes()
            //        .Where(x => typeof(TService).IsAssignableFrom(x))
            //        where t.GetConstructors()
            //            .Any(c => c.GetParameters()
            //                  .Any(p => p.ParameterType.IsInterface))
            //        select t;


            var type = typeof(TService);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.Name.Equals(type.Name));

            // In this project we are just going to get the first available type for now
            var instance =  Activator.CreateInstance(types.FirstOrDefault());

            _registeredSingletons.Add(typeof(TService), () => instance);

        }

        /// <summary>
        /// Registers a type based on the instance provided
        /// </summary>
        /// <typeparam name="TService">The type to be registered against</typeparam>
        /// <param name="instance">The instance to map to</param>
        public void AddSingleton<TService>(TService instance)
        {
            _registeredSingletons.Add(typeof(TService), () => instance);
        }

        public void AddSingleton<TService, TConcrete>() where TConcrete : TService
        {
            var concreteInstance = Activator.CreateInstance<TConcrete>();
            _registeredSingletons.Add(typeof(TService), () => concreteInstance);
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
            return new ContainerRuntime(this);
        }

        /// <summary>
        /// Returns the keys registerd in our internal dictionary. Added for validating the container in tests
        /// </summary>
        /// <returns>A collection of keys in for the registration</returns>
        public Dictionary<Type, Func<object>>.KeyCollection GetRegisteredSingletonTypes()
        {
            return _registeredSingletons.Keys;
        }

        /// <summary>
        /// Returns the keys registerd in our internal dictionary. Added for validating the container
        /// Returns the keys registerd in our internal dictionary. Added for validating the container in tests
        /// </summary>
        /// <returns>A collection of keys in for the registration</returns>
        public Dictionary<Type, Func<object>>.KeyCollection GetRegisteredSingletonTypes()
        {
            return null;
        }
    }
}
