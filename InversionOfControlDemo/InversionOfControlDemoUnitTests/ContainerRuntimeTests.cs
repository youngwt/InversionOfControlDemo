using InversionOfControlDemo;
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
    }
}
