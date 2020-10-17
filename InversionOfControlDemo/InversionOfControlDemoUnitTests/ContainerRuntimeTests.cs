using InversionOfControlDemo;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace InversionOfControlDemoUnitTests
{
    [TestFixture]
    public class ContainerRuntimeTests
    {
        [Test]
        public void GetService__can_return_transient_service__returns_object()
        {
            // Arrange

            var container = new ContainerRuntime();

            // Act

            var options = container.GetService<MyOptions>();

            // Assert
            Assert.That(options, Is.InstanceOf<MyOptions>());


        }
    }
}
