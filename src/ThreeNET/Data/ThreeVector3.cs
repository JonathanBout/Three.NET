using System.Numerics;
using System.Text.Json.Serialization;
using ThreeNET.Helpers;

namespace ThreeNET.Data
{
	public readonly struct ThreeVector3 : IThreeVector<ThreeVector3>
	{
		public ThreeVector3()
			:this(0, 0, 0) { }

		public ThreeVector3(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		[JsonPropertyName("x")]
		public float X { get; }
		[JsonPropertyName("y")]
		public float Y { get; }
		[JsonPropertyName("z")]
		public float Z { get; }
		public float Magnitude => MathF.Sqrt(MagnitudeSquared);
		public float MagnitudeSquared => X * X + Y * Y + Z * Z;

		private static ThreeVector3? zero;
		private static ThreeVector3? one;
		private static ThreeVector3? up;
		private static ThreeVector3? left;
		private static ThreeVector3? forward;
		public static ThreeVector3 Zero => zero ??= new ThreeVector3(0, 0, 0);
		public static ThreeVector3 One => one ??= new ThreeVector3(1, 1, 1);
		public static ThreeVector3 Up => up ??= new ThreeVector3(0, 1, 0);
		public static ThreeVector3 Left => left ??= new ThreeVector3(1, 0, 1);
		public static ThreeVector3 Forward => forward ??= new ThreeVector3(0, 0, 1);
		public override int GetHashCode() => HashCode.Combine(X, Y, Z);
		public override bool Equals(object? obj)
			=> obj is ThreeVector3 vector && Equals(vector);

        public ThreeVector3 Normalize()
        {
            var magnitude = Magnitude;
            return new ThreeVector3(X / magnitude, Y / magnitude, Z / magnitude);
        }

        public bool Equals(ThreeVector3 other)
            => MathHelper.Equal(X, other.X)
               && MathHelper.Equal(Y, other.Y)
               && MathHelper.Equal(Z, other.Z);

        public int CompareTo(ThreeVector3 other)
            => (int)DistanceSquared(this, other);

        public static ThreeVector3 operator +(ThreeVector3 left, ThreeVector3 right)
            => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

        public static ThreeVector3 operator -(ThreeVector3 left, ThreeVector3 right)
            => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

        public static ThreeVector3 operator *(ThreeVector3 left, ThreeVector3 right)
            => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

        public static ThreeVector3 operator *(float left, ThreeVector3 right)
            => new(left * right.X, left * right.Y, left * right.Z);

        public static ThreeVector3 operator *(ThreeVector3 left, float right)
            => right * left;

        public static ThreeVector3 operator /(ThreeVector3 left, ThreeVector3 right)
            => new(left.X / right.X, left.Y / right.Y, left.Z / right.Z);

        public static ThreeVector3 operator /(float left, ThreeVector3 right)
            => new(left / right.X, left / right.Y, left / right.Z);

        public static ThreeVector3 operator /(ThreeVector3 left, float right)
            => new(left.X / right, left.Y / right, left.Z / right);

        public static bool operator ==(ThreeVector3 left, ThreeVector3 right)
            => left.Equals(right);

        public static bool operator !=(ThreeVector3 left, ThreeVector3 right)
            =>  !(left == right);

		public static float Distance(ThreeVector3 a, ThreeVector3 b)
			=> (a - b).Magnitude;
		public static float DistanceSquared(ThreeVector3 a, ThreeVector3 b)
			=> (a - b).MagnitudeSquared;
	}

	public static class Euler
	{
		const float DegToRad = MathF.PI / 180;
		public static ThreeVector3 FromDegrees(float x = 0, float y = 0, float z = 0)
		{
			return new ThreeVector3(x * DegToRad, y * DegToRad, z * DegToRad);
		}

		public static ThreeVector3 FromRadians(float x = 0, float y = 0, float z = 0)
		{
			return new ThreeVector3(x, y, z);
		}
	}
}