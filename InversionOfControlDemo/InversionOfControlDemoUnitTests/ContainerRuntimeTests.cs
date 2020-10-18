using InversionOfControlDemo;
using InversionOfControlDemo.Implementations;
using InversionOfControlDemo.Interfaces;
using NUnit.Framework;

namespace InversionOfControlDemoUnitTests
{
    [TestFixture]
    public class ContainerRuntimeTests
    {
        [Test]
        public void GetService__returns_singleton()
        {
            // Arrange
            var container = new ContainerRegistration();
            container.AddSingleton<IVatRates>();
            var runtime = container.CreateRuntime();

            // Act
            var vatRates = runtime.GetService<IVatRates>();

            // Assert
            Assert.That(vatRates, Is.Not.Null);
            Assert.That(vatRates, Is.InstanceOf<SqlVatRates>());

        }

        [Test]
        public void GetService_with_type_parameter__returns_singleton()
        {
            // Arrange
            var container = new ContainerRegistration();
            container.AddSingleton<IVatRates>();
            var runtime = container.CreateRuntime();

            // Act
            var vatRates = runtime.GetService(typeof(IVatRates));

            // Assert
            Assert.That(vatRates, Is.Not.Null);
            Assert.That(vatRates, Is.InstanceOf<SqlVatRates>());

        }

        [Test]
        public void GetService__returns_transient()
        {
            // Arrange
            var container = new ContainerRegistration();
            container.AddTransient<IVatRates>();
            var runtime = container.CreateRuntime();

            // Act
            var vatRates = runtime.GetService<IVatRates>();

            // Assert
            Assert.That(vatRates, Is.Not.Null);
            Assert.That(vatRates, Is.InstanceOf<SqlVatRates>());

        }

        [Test]
        public void GetService_with_type_parameter__returns_transient()
        {
            // Arrange
            var container = new ContainerRegistration();
            container.AddTransient<IVatRates>();
            var runtime = container.CreateRuntime();

            // Act
            var vatRates = runtime.GetService(typeof(IVatRates));

            // Assert
            Assert.That(vatRates, Is.Not.Null);
            Assert.That(vatRates, Is.InstanceOf<SqlVatRates>());
        }

        [Test]
        public void GetService__returns_singleton_with_dependency()
        {
            // Arrange
            var container = new ContainerRegistration();
            container.AddSingleton<IB>();
            var runtime = container.CreateRuntime();

            // Act
            var b = runtime.GetService(typeof(IB));

            // Assert
            Assert.That(b, Is.Not.Null);
            Assert.That(b, Is.InstanceOf<B>());
        }

        [Test]
        public void GetService__returns_transient_with_dependency()
        {
            // Arrange
            var container = new ContainerRegistration();
            container.AddTransient<IB>();
            var runtime = container.CreateRuntime();

            // Act
            var b = runtime.GetService(typeof(IB));

            // Assert
            Assert.That(b, Is.Not.Null);
            Assert.That(b, Is.InstanceOf<B>());
        }
    }
}
