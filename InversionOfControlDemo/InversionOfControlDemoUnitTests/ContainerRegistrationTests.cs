using InversionOfControlDemo;
using NUnit.Framework;

namespace InversionOfControlDemoUnitTests
{
    public class Tests
    {

        private ContainerRegistration _containerUnderTest;

        [SetUp]
        public void Setup()
        {
            _containerUnderTest = new ContainerRegistration();
        }

        [Test]
        public void Test_CI_CD_Pipeline__Returns_Pass()
        {
            Assert.Pass();
        }

        [Test]
        public void CreateRuntime__returns_a_runtime()
        {
            // Arrange -- see setup

            // Act
            var runtime = _containerUnderTest.CreateRuntime();

            // Assert
            Assert.That(runtime, Is.Not.Null);
            Assert.That(runtime, Is.InstanceOf<ContainerRuntime>());
        }

        [Test]
        public void AddService__Can_add_singleton_service__container_contains_type()
        {
            // Arrange
            var containerRegistration = new ContainerRegistration();
            var sqlVatRates = new SqlVatRates();

            // Act
            containerRegistration.AddSingleton<IVatRates>(sqlVatRates);
            var keys = containerRegistration.GetRegisteredSingletonTypes();

            // Assert
            Assert.That(keys, Is.Not.Null);
            Assert.That(keys.Count, Is.EqualTo(1));
            
            foreach(var key in keys)
            {
                Assert.That(key.Name, Is.EqualTo("IVatRates"));
            }
            
        }
    }
}