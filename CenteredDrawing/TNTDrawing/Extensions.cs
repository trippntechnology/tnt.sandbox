using System;

namespace TNTDrawing
{
	/// <summary>
	/// Extension methods
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Kotlin let
		/// </summary>
		public static R Let<T, R>(this T value, Func<T, R> block)
		{
			if (value == null) return default;
			return block(value);
		}

		/// <summary>
		/// Kotlin also
		/// </summary>
		public static T Also<T>(this T value, Action<T> block)
		{
			if (value == null) return default;
			block(value);
			return value;
		}
	}
}
