using System;
using System.Collections.Generic;
using System.Text;

namespace InversionOfControlDemo
{
    public interface IContainerRuntime
    {
        TService GetService<TService>();
        object GetService(Type serviceType);
    }
}
