namespace ThreeNET.Test
{
    public class ThreeVector2Tests
    {
        private Vector2 vector;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            vector = new Vector2(10, 11);
        }

        [Test]
        public void Equals()
        {
            var b = new Vector2(vector.X, vector.Y);
            Assert.That(vector, Is.EqualTo(b));
        }

        [Test]
        public void NotEquals()
        {
            var b = new Vector2(vector.X + 1, vector.Y - 1);
            Assert.That(vector, Is.Not.EqualTo(b));
        }

        [Test]
        public void Add()
        {
            var b = new Vector2(50, 40);
            var result = vector + b;
            Assert.Multiple(() =>
            {
                Assert.That(result.X, Is.EqualTo(60));
                Assert.That(result.Y, Is.EqualTo(51));
            });
        }
    }
}