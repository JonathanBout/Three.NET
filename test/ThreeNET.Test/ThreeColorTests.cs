namespace ThreeNET.Test
{
	public class ThreeColorTests
	{
		[Test]
		public void ThreeColorFromInteger()
		{
			int hexColor = 0xff7fff;
			decimal red = 1m;
			decimal green = .5m;
			decimal blue = 1m;
			var threeColor = (ThreeColor)hexColor;
			Assert.Multiple(() =>
			{
				Assert.That(threeColor.Red, Is.EqualTo(red));
				Assert.That(threeColor.Green, Is.EqualTo(green));
				Assert.That(threeColor.Blue, Is.EqualTo(blue));
			});
		}

		[Test]
		public void ThreeColorToInteger()
		{
			var threeColor = new ThreeColor(.5m, 1, .5m);
			int hexColor = (int)threeColor;
			string value = hexColor.ToString("X");
			Assert.That(value, Is.EqualTo("7FFF7F"));
		}
	}
}