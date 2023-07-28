namespace ThreeNET.Data
{
	public static class Euler
	{
		private const float DegToRad = MathF.PI / 180;
		public static Vector3 FromDegrees(float x = 0, float y = 0, float z = 0)
		{
			return new Vector3(x * DegToRad, y * DegToRad, z * DegToRad);
		}

		public static Vector3 FromRadians(float x = 0, float y = 0, float z = 0)
		{
			return new Vector3(x, y, z);
		}
	}
}