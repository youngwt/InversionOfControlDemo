using InversionOfControlDemo;
using NUnit.Framework;
using System;
using System.Collections.Generic;

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
        public void AddSingleton_With_Instance__container_contains_type()
        {
            // Arrange
            var sqlVatRates = new SqlVatRates();

            // Act
            _containerUnderTest.AddSingleton<IVatRates>(sqlVatRates);
            var registeredTypes = _containerUnderTest.GetRegisteredSingletonTypes();

            // Assert
            ValidateContainerByRegisteredTypes(registeredTypes, new List<String> { "IVatRates" });

        }

        [Test]
        public void AddSingleton_With_Reference_To_Instance__container_contains_type()
        {
            // Arrange

            // Act
            _containerUnderTest.AddSingleton<IVatRates, SqlVatRates>();
            var registeredTypes = _containerUnderTest.GetRegisteredSingletonTypes();

            // Assert
            ValidateContainerByRegisteredTypes(registeredTypes, new List<String> { "IVatRates" });

        }

        [Test]
        public void AddSingleton_With_Interface_only()
        {
            // Arrange

            // Act
            _containerUnderTest.AddSingleton<IVatRates>();
            var registeredTypes = _containerUnderTest.GetRegisteredSingletonTypes();

            // Assert
            ValidateContainerByRegisteredTypes(registeredTypes, new List<String> { "IVatRates" });
        }

        [Test]
        public void AddSignleton_With_Factory()
        {
            // Arrange

            // Act
            //_containerUnderTest.AddSingleton()

            // Assert
            Assert.Fail();
        }

        [Test]
        public void AddTransient_Without_Instance()
        {
            // Arrange
            var sqlRates = new SqlVatRates();

            // Act
            _containerUnderTest.AddTransient<IVatRates>();

            // Assert
        }

        [Test]
        public void AddTransient_With_ConcreteInstance()
        {
            // Arrange

            // Act
            _containerUnderTest.AddTransient<IVatRates, SqlVatRates>();

            // Assert
        }

        public void AddTransient_With_Concrete_And_Factory()
        {
            // Arrange

            // Act
            _containerUnderTest.AddTransient<IVatRates, SqlVatRates>(sp => new SqlVatRates());

            // Assert
        }

        public void AddTransiet_With_Type_and_Factory()
        {
            // Arrange

            // Act
            _containerUnderTest.AddTransient<IVatRates>(sp => new SqlVatRates());

            // Assert
        }

        /// <summary>
        /// Helper method to run several asserts on the results of the container action
        /// </summary>
        /// <param name="registeredTypes">A Keycollection from the container</param>
        /// <param name="expectedTypeNames">A list of names expected to exist in the container</param>
        private void ValidateContainerByRegisteredTypes(Dictionary<Type, Func<object>>.KeyCollection registeredTypes, IList<string> expectedTypeNames)
        {
            Assert.That(registeredTypes, Is.Not.Null);
            Assert.That(registeredTypes.Count, Is.EqualTo(1));

            foreach (var key in registeredTypes)
            {
                Assert.That(expectedTypeNames.Contains(key.Name), Is.True);
            }
        }
    }
}