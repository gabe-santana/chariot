using CGS.Handler.Utils;

namespace CGS.UnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ConnectionParserTest()
        {
            var p = Parser.GetParams("conn(x,y)");

            Assert.That(p, Is.EqualTo(new string[] { "x", "y" }));
        }
    }
}