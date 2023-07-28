using System.Globalization;
using System.Numerics;
using System.Text.Json.Serialization;
using ThreeNET.Helpers;

namespace ThreeNET.Data
{
	public readonly struct Vector3 : IVector<Vector3>
	{
		public Vector3()
			:this(0, 0, 0) { }

		public Vector3(float x, float y, float z)
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

		private static Vector3? zero;
		private static Vector3? one;
		private static Vector3? up;
		private static Vector3? left;
		private static Vector3? forward;
		public static Vector3 Zero => zero ??= new Vector3(0, 0, 0);
		public static Vector3 One => one ??= new Vector3(1, 1, 1);
		public static Vector3 Up => up ??= new Vector3(0, 1, 0);
		public static Vector3 Left => left ??= new Vector3(1, 0, 1);
		public static Vector3 Forward => forward ??= new Vector3(0, 0, 1);
		public override int GetHashCode() => HashCode.Combine(X, Y, Z);
		public override bool Equals(object? obj)
			=> obj is Vector3 vector && Equals(vector);

        public Vector3 Normalize()
        {
            var magnitude = Magnitude;
            return new Vector3(X / magnitude, Y / magnitude, Z / magnitude);
        }

        public bool Equals(Vector3 other)
            => MathHelper.Equal(X, other.X)
               && MathHelper.Equal(Y, other.Y)
               && MathHelper.Equal(Z, other.Z);

        public int CompareTo(Vector3 other)
            => (int)DistanceSquared(this, other);

		public string ToString(string? format, IFormatProvider? formatProvider)
		{
			if (format is not null)
			{
				string result = "";
				foreach (var formatChar in format)
				{
					result += char.ToLowerInvariant(formatChar) switch
					{
						'x' => X.ToString(formatProvider),
						'y' => Y.ToString(formatProvider),
						'z' => Z.ToString(formatProvider),
						'p' => $"({CommaString(formatProvider)})",
						'b' => $"[{CommaString(formatProvider)}]",
						'a' => $"<{CommaString(formatProvider)}>",
						'r' => CommaString(formatProvider),
						_ => formatChar.ToString(formatProvider)
					};
				}
				return result;
			}

			return ToString();
		}

		string CommaString(IFormatProvider? formatProvider)
		{
			if (formatProvider is null)
			{
				return $"{X}, {Y}, {Z}";
			}
			return $"{X.ToString(formatProvider)}, {Y.ToString(formatProvider)}, {Z.ToString(formatProvider)}";
		}

		public override string ToString()
		{
			return ToString("p", CultureInfo.CurrentCulture);
		}

		public static Vector3 operator +(Vector3 left, Vector3 right)
            => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

        public static Vector3 operator -(Vector3 left, Vector3 right)
            => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

        public static Vector3 operator *(Vector3 left, Vector3 right)
            => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

        public static Vector3 operator *(float left, Vector3 right)
            => new(left * right.X, left * right.Y, left * right.Z);

        public static Vector3 operator *(Vector3 left, float right)
            => right * left;

        public static Vector3 operator /(Vector3 left, Vector3 right)
            => new(left.X / right.X, left.Y / right.Y, left.Z / right.Z);

        public static Vector3 operator /(float left, Vector3 right)
            => new(left / right.X, left / right.Y, left / right.Z);

        public static Vector3 operator /(Vector3 left, float right)
            => new(left.X / right, left.Y / right, left.Z / right);

        public static bool operator ==(Vector3 left, Vector3 right)
            => left.Equals(right);

        public static bool operator !=(Vector3 left, Vector3 right)
            =>  !(left == right);

		public static float Distance(Vector3 a, Vector3 b)
			=> (a - b).Magnitude;
		public static float DistanceSquared(Vector3 a, Vector3 b)
			=> (a - b).MagnitudeSquared;

	}
}