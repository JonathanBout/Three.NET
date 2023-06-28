using System.Text.Json.Serialization;

namespace ThreeNET.Data
{
	public readonly struct ThreeVector2 : IThreeVector<ThreeVector2>
	{
		public ThreeVector2(float x, float y)
		{
			X = x;
			Y = y;
		}

		[JsonPropertyName("x")]
		public float X { get; }
		[JsonPropertyName("y")]
		public float Y { get; }
		public float MagnitudeSquared => X * X + Y * Y;

		public float Magnitude => MathF.Sqrt(MagnitudeSquared);

		public override bool Equals(object? obj)
			=> obj is ThreeVector2 vector && Equals(vector);
		public override int GetHashCode() => HashCode.Combine(X, Y);
		
		public ThreeVector2 Normalize()
		{
			var magnitude = Magnitude;
			return new ThreeVector2(X / magnitude, Y / magnitude);
		}
		public bool Equals(ThreeVector2 other)
			=> other.X == X && other.Y == Y;
		public int CompareTo(ThreeVector2 other) => (int)DistanceSquared(this, other);

		public static ThreeVector2 operator +(ThreeVector2 left, ThreeVector2 right)
		{
			return new ThreeVector2(left.X + right.X, left.Y + right.Y);
		}
		public static ThreeVector2 operator -(ThreeVector2 left, ThreeVector2 right)
		{
			return new ThreeVector2(left.X - right.X, left.Y - right.Y);
		}
		public static ThreeVector2 operator *(ThreeVector2 left, ThreeVector2 right)
		{
			return new ThreeVector2(left.X * right.X, left.Y * right.Y);
		}
		public static ThreeVector2 operator *(float left, ThreeVector2 right)
		{
			return new ThreeVector2(left * right.X, left * right.Y);
		}
		public static ThreeVector2 operator *(ThreeVector2 left, float right)
		{
			return right * left;
		}
		public static ThreeVector2 operator /(ThreeVector2 left, ThreeVector2 right)
		{
			return new ThreeVector2(left.X / right.X, left.Y / right.Y);
		}
		public static ThreeVector2 operator /(float left, ThreeVector2 right)
		{
			return new ThreeVector2(left / right.X, left / right.Y);
		}
		public static ThreeVector2 operator /(ThreeVector2 left, float right)
		{
			return new ThreeVector2(left.X / right, left.Y / right);
		}
		public static bool operator ==(ThreeVector2 left, ThreeVector2 right)
		{
			return left.Equals(right);
		}
		public static bool operator !=(ThreeVector2 left, ThreeVector2 right)
		{
			return !(left == right);
		}

		public static float Distance(ThreeVector2 a, ThreeVector2 b)
			=> (a - b).Magnitude;
		public static float DistanceSquared(ThreeVector2 a, ThreeVector2 b)
			=> (a - b).MagnitudeSquared;
	}
}