namespace ThreeNET.Data
{
	/// <summary>
	/// The base for all ThreeJS interop vectors.
	/// </summary>
	/// <typeparam name="TSelf">The type of the vector itself.</typeparam>
	internal interface IVector<TSelf> : IEquatable<TSelf>, IComparable<TSelf>, IFormattable where TSelf : IVector<TSelf>
	{
		public static abstract TSelf Zero { get; }
		public static abstract TSelf One { get; }
		public static abstract TSelf Up { get; }
		public static abstract TSelf Left { get; }
		public static abstract TSelf Forward { get; }
		public float Magnitude { get; }
		public float MagnitudeSquared { get; }
		public TSelf Normalize();

		public static abstract TSelf operator +(TSelf left, TSelf right);
		public static abstract TSelf operator -(TSelf left, TSelf right);
		public static abstract TSelf operator *(TSelf left, TSelf right);
		public static abstract TSelf operator *(float left, TSelf right);
		public static abstract TSelf operator *(TSelf left, float right);
		public static abstract TSelf operator /(TSelf left, TSelf right);
		public static abstract TSelf operator /(float left, TSelf right);
		public static abstract TSelf operator /(TSelf left, float right);
		public static abstract bool operator ==(TSelf left, TSelf right);
		public static abstract bool operator !=(TSelf left, TSelf right);

		public static abstract float Distance(TSelf a, TSelf b);
		public static abstract float DistanceSquared(TSelf a, TSelf b);
	}
}