namespace ThreeNET.Data
{
	internal class ThreeColor
	{
		public ThreeColor() { }
		public ThreeColor(double r, double g, double b)
		{
			Red = r;
			Green = g;
			Blue = b;
		}
		public double Red { get; set; } = 1;
		public double Green { get; set; } = 1;
		public double Blue { get; set; } = 1;

		public static implicit operator ThreeColor(int value)
		{
			if (value > 16_777_215)
				throw new ArgumentOutOfRangeException(nameof(value));
			var bytes = BitConverter.GetBytes(value);
			if (BitConverter.IsLittleEndian)
				bytes = bytes.Reverse().ToArray();
			bytes = bytes[..3];
			var r = bytes[0] / 255.0;
			var g = bytes[1] / 255.0;
			var b = bytes[2] / 255.0;
			return new ThreeColor(r, g, b);
		}

		public static explicit operator int(ThreeColor color)
		{
			var r = (byte)(color.Red * 255);
			var g = (byte)(color.Green * 255);
			var b = (byte)(color.Blue * 255);
			var bytes = new byte[] { r, g, b, 0x0 };
			if (BitConverter.IsLittleEndian)
				bytes = bytes.Reverse().ToArray();
			return BitConverter.ToInt32(bytes);
		}

		public override string ToString()
		{
			return "#" + ((int)this).ToString("X6");
		}
	}
}
