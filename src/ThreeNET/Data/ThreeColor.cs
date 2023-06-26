namespace ThreeNET.Data
{
	public class ThreeColor
	{
		public ThreeColor() { }
		public ThreeColor(decimal r, decimal g, decimal b)
		{
			Red = r;
			Green = g;
			Blue = b;
		}
		public decimal Red { get; set; } = 1;
		public decimal Green { get; set; } = 1;
		public decimal Blue { get; set; } = 1;

		public static implicit operator ThreeColor(int value)
		{
			if (value > 16_777_215)
				throw new ArgumentOutOfRangeException(nameof(value));
			var bytes = BitConverter.GetBytes(value);
			if (!BitConverter.IsLittleEndian)
				bytes = bytes.Reverse().ToArray();
			bytes = bytes[..3];
			var r = Math.Round(bytes[0] / 255.0m, 2);
			var g = Math.Round(bytes[1] / 255.0m, 2);
			var b = Math.Round(bytes[2] / 255.0m, 2);
			return new ThreeColor(r, g, b);
		}

		public static explicit operator int(ThreeColor color)
		{
			var r = (byte)(Math.Round(color.Red, 2) * 255);
			var g = (byte)(Math.Round(color.Green, 2) * 255);
			var b = (byte)(Math.Round(color.Blue, 2) * 255);
			var bytes = new byte[] { r, g, b, 0x0 };
			if (!BitConverter.IsLittleEndian)
				bytes = bytes.Reverse().ToArray();
			return BitConverter.ToInt32(bytes);
		}

		public override string ToString()
		{
			return "#" + ((int)this).ToString("X6");
		}
	}
}
