namespace ThreeNET.Data
{
	internal interface IThreeVector<TSelf> : IEquatable<TSelf>, IComparable<TSelf> where TSelf : IThreeVector<TSelf>
	{
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