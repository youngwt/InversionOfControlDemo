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
    }
}