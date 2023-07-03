namespace ThreeNET.Test
{
    public class ThreeVector3Tests
    {
        private ThreeVector3 vector;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            vector = new ThreeVector3(10, 11, 12);
        }

        [Test]
        public void Equals()
        {
            var b = new ThreeVector3(vector.X, vector.Y, vector.Z);
            Assert.That(vector, Is.EqualTo(b));
        }

        [Test]
        public void NotEquals()
        {
            var b = new ThreeVector3(vector.X + 1, vector.Y - 1, vector.Z);
            Assert.That(vector, Is.Not.EqualTo(b));
        }

        [Test]
        public void Add()
        {
            var b = new ThreeVector3(50, 40, 30);
            var result = vector + b;
            Assert.Multiple(() =>
            {
                Assert.That(result.X, Is.EqualTo(60));
                Assert.That(result.Y, Is.EqualTo(51));
                Assert.That(result.Z, Is.EqualTo(42));
            });
        }
    }
}