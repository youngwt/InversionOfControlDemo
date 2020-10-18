using InversionOfControlDemo.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace InversionOfControlDemo.Implementations
{
    class B : IB
    {
        private A A {get; set; }

        public B(A a)
        {
            A = a;
        }
    }
}
