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

    // Service -&gt; Concrete
    void AddTransient<TService, TConcrete>() where TConcrete : TService;

    // Service -&gt; Concrete factory method
    void AddTransient<TService, TConcrete>(Func<IContainerRuntime, TConcrete> factoryMethod) where TConcrete : TService;
    }
}
