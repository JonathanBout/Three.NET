using System.Globalization;
using System.Text.Json.Serialization;

namespace ThreeNET.Data
{
	public class Vector2 : IVector<Vector2>
	{
		public Vector2()
			:this(0, 0) { }
		public Vector2(float x, float y)
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

		private static Vector2? zero;
		private static Vector2? one;
		private static Vector2? up;
		private static Vector2? left;
		public static Vector2 Zero => zero ??= new Vector2(0, 0);

		public static Vector2 One => one ??= new Vector2(1, 1);

		public static Vector2 Up => up ??= new Vector2(0, 1);

		public static Vector2 Left => left ??= new Vector2(1, 0);

		public static Vector2 Forward => Zero;

		public override bool Equals(object? obj)
			=> obj is Vector2 vector && Equals(vector);
		public override int GetHashCode() => HashCode.Combine(X, Y);
		
		public Vector2 Normalize()
		{
			var magnitude = Magnitude;
			return new Vector2(X / magnitude, Y / magnitude);
		}
		public bool Equals(Vector2? other)
			=> other is not null && other.X == X && other.Y == Y;
		public int CompareTo(Vector2? other) => (int)(other is null ? MagnitudeSquared : DistanceSquared(this, other));


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
				return $"{X}, {Y}";
			}
			return $"{X.ToString(formatProvider)}, {Y.ToString(formatProvider)}";
		}

		public override string ToString()
		{
			return ToString("p", CultureInfo.CurrentCulture);
		}


		public static Vector2 operator +(Vector2 left, Vector2 right)
		{
			return new Vector2(left.X + right.X, left.Y + right.Y);
		}
		public static Vector2 operator -(Vector2 left, Vector2 right)
		{
			return new Vector2(left.X - right.X, left.Y - right.Y);
		}
		public static Vector2 operator *(Vector2 left, Vector2 right)
		{
			return new Vector2(left.X * right.X, left.Y * right.Y);
		}
		public static Vector2 operator *(float left, Vector2 right)
		{
			return new Vector2(left * right.X, left * right.Y);
		}
		public static Vector2 operator *(Vector2 left, float right)
		{
			return right * left;
		}
		public static Vector2 operator /(Vector2 left, Vector2 right)
		{
			return new Vector2(left.X / right.X, left.Y / right.Y);
		}
		public static Vector2 operator /(float left, Vector2 right)
		{
			return new Vector2(left / right.X, left / right.Y);
		}
		public static Vector2 operator /(Vector2 left, float right)
		{
			return new Vector2(left.X / right, left.Y / right);
		}
		public static bool operator ==(Vector2 left, Vector2 right)
		{
			return left.Equals(right);
		}
		public static bool operator !=(Vector2 left, Vector2 right)
		{
			return !(left == right);
		}

		public static float Distance(Vector2 a, Vector2 b)
			=> (a - b).Magnitude;
		public static float DistanceSquared(Vector2 a, Vector2 b)
			=> (a - b).MagnitudeSquared;
	}
}