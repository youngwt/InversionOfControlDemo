﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

// There is a single internal method that is only to be used by unit tests
[assembly: InternalsVisibleTo("InversionOfControlDemoUnitTests")]
namespace InversionOfControlDemo
{
    /// <summary>
    /// Contains the mappings of interfaces to implementations
    /// </summary>
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
            var type = GetTypeFromInterface<TService>();
            var instance = Resolve(type);
 
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
            //_registeredTypes.Add(typeof(TService), () =>
            //{

            //});

            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds the means to create transient instances given a type
        /// </summary>
        /// <typeparam name="TService">The type to map to</typeparam>
        public void AddTransient<TService>()
        {
            var type = GetTypeFromInterface<TService>();

            _registeredTypes.Add(typeof(TService), () =>
            {
                return Resolve(type);
            });
        }

        /// <summary>
        /// Add a means to create a transient instance using a factory method
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="factoryMethod"></param>
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
            _registeredTypes.Add(typeof(TService), () =>
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
        /// Resolves a registered type
        /// </summary>
        /// <param name="type">The type to get</param>
        /// <returns></returns>
        public object Resolve(Type type)
        {
            if(_registeredTypes.ContainsKey(type))
            {
                return _registeredTypes[type]();
            }

            var constructor = type.GetConstructors()
                .OrderByDescending(c => c.GetParameters().Length)
                .First();

            var args = constructor.GetParameters()
                .Select(param => Resolve(param.ParameterType))
                .ToArray();

            return Activator.CreateInstance(type, args);
        }

        /// <summary>
        /// Gets a concrete implementation based on a provided interface
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        private Type GetTypeFromInterface<TService>()
        {
            var interfaceType = typeof(TService);
            var implementingTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => interfaceType.IsAssignableFrom(p) && !p.Name.Equals(interfaceType.Name));

            // for now we are just going to return the first type found (if any)
            return implementingTypes.FirstOrDefault();
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
