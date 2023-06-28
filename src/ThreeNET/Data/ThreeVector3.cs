using System.Numerics;
using System.Text.Json.Serialization;

namespace ThreeNET.Data
{
	public readonly struct ThreeVector3 : IThreeVector<ThreeVector3>
	{
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


		public override int GetHashCode() => HashCode.Combine(X, Y, Z);
		public override bool Equals(object? obj)
			=> obj is ThreeVector3 vector && Equals(vector);

		public ThreeVector3 Normalize()
		{
			var magnitude = Magnitude;
			return new ThreeVector3(X / magnitude, Y / magnitude, Z / magnitude);
		}
		public bool Equals(ThreeVector3 other)
			=> other.X == X && other.Y == Y && other.Z == Z;
		public int CompareTo(ThreeVector3 other) => (int)DistanceSquared(this, other);

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
		{
			return left.Equals(right);
		}
		public static bool operator !=(ThreeVector3 left, ThreeVector3 right)
		{
			return !(left == right);
		}

		public static float Distance(ThreeVector3 a, ThreeVector3 b)
			=> (a - b).Magnitude;
		public static float DistanceSquared(ThreeVector3 a, ThreeVector3 b)
			=> (a - b).MagnitudeSquared;
	}
}