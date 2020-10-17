using System;
using System.Collections.Generic;
using System.Text;

namespace InversionOfControlDemo
{
    public interface IContainerRegistration
    {
        // Transient service registration methods

        // Service
        void AddTransient<TService>();

        // Service factory method
        void AddTransient<TService>(Func<IContainerRuntime, TService> factoryMethod);

        // Service -> Concrete
        void AddTransient<TService, TConcrete>() where TConcrete : TService;

        // Service -> Concrete factory method
        void AddTransient<TService, TConcrete>(Func<IContainerRuntime, TConcrete> factoryMethod) where TConcrete : TService;


        // Singleton service registration methods

        // Service
        void AddSingleton<TService>();

        // Service instance
        void AddSingleton<TService>(TService instance);

        // Service -> Concrete
        void AddSingleton<TService, TConcrete>() where TConcrete : TService;

        // Service factory method
        void AddSingleton<TService>(Func<IContainerRuntime, TService> factoryMethod);

        IContainerRuntime CreateRuntime();
    }
}
