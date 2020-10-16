using NUnit.Framework;

namespace InversionOfControlDemoUnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_CI_CD_Pipeline__Returns_Pass()
        {
            Assert.Pass();
        }

        [Test]
        public void Intentionally_Failing_Test()
        {
            Assert.Fail();
        }
    }
}