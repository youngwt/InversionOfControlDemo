using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

// There is a single internal method that is only to be used by unit tests
[assembly: InternalsVisibleTo("InversionOfControlDemoUnitTests")]
namespace InversionOfControlDemo
{    
    public class ContainerRegistration : IContainerRegistration
    {
        /// <summary>
        /// Container that maps Types to object returning functions
        /// </summary>
        private static Dictionary<Type, Func<object>> _registeredTypes;

        public ContainerRegistration()
        {
            _registeredTypes = new Dictionary<Type, Func<object>>();
        }

        /// <summary>
        /// Registers a singleton based on the provided type
        /// </summary>
        /// <typeparam name="TService">The type to map</typeparam>
        public void AddSingleton<TService>()
        {
            var type = typeof(TService);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.Name.Equals(type.Name));

            // In this project we are just going to get the first available type for now
            var instance =  Activator.CreateInstance(types.FirstOrDefault());

            _registeredTypes.Add(typeof(TService), () => instance);

        }

        /// <summary>
        /// Registers a type based on the instance provided
        /// </summary>
        /// <typeparam name="TService">The type to be registered against</typeparam>
        /// <param name="instance">The instance to map to</param>
        public void AddSingleton<TService>(TService instance)
        {
            _registeredTypes.Add(typeof(TService), () => instance);
        }

        /// <summary>
        /// Registers a signleton based on a concrete type provided
        /// </summary>
        /// <typeparam name="TService">The base interface type</typeparam>
        /// <typeparam name="TConcrete">The implementation to map to</typeparam>
        public void AddSingleton<TService, TConcrete>() where TConcrete : TService
        {
            var concreteInstance = Activator.CreateInstance<TConcrete>();
            _registeredTypes.Add(typeof(TService), () => concreteInstance);
        }

        /// <summary>
        /// Registers a singleton by mapping a factory method to a type
        /// </summary>
        /// <typeparam name="TService">The type to map to</typeparam>
        /// <param name="factoryMethod">The factory Method to map</param>
        public void AddSingleton<TService>(Func<IContainerRuntime, TService> factoryMethod)
        {
            //_registeredTypes.Add(typeof(TService), factoryMethod);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds the means to create transient instances given a type
        /// </summary>
        /// <typeparam name="TService">The type to map to</typeparam>
        public void AddTransient<TService>()
        {
            throw new NotImplementedException();
        }

        public void AddTransient<TService>(Func<IContainerRuntime, TService> factoryMethod)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Registers a means to create transient instances of a given type with a provided implementation
        /// </summary>
        /// <typeparam name="TService">The type to map to </typeparam>
        /// <typeparam name="TConcrete">The implementation</typeparam>
        public void AddTransient<TService, TConcrete>() where TConcrete : TService
        {
            _registeredTypes.Add(typeof(TConcrete), () =>
            {
                return Activator.CreateInstance<TConcrete>();
            });
        }

        /// <summary>
        /// Adds a means to create a transient instance of a given type when provided with a concrete implementation
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TConcrete"></typeparam>
        /// <param name="factoryMethod"></param>
        public void AddTransient<TService, TConcrete>(Func<IContainerRuntime, TConcrete> factoryMethod) where TConcrete : TService
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a new runtime instance that maps to this container
        /// </summary>
        /// <returns></returns>
        public IContainerRuntime CreateRuntime()
        {
            return new ContainerRuntime(this);
        }

        /// <summary>
        /// Returns the keys registerd in our internal dictionary. Added for validating the container in tests
        /// </summary>
        /// <returns>A collection of keys in for the registration</returns>        
        internal Dictionary<Type, Func<object>>.KeyCollection GetRegisteredSingletonTypes()
        {
            return _registeredTypes.Keys;
        }
    }
}
