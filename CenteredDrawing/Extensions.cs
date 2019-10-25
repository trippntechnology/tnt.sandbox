﻿using System;
using System.Drawing;
using System.Windows;

namespace CenteredDrawing
{
	public static class Extensions
	{
		public static R Let<T, R>(this T value, Func<T, R> block)
		{
			if (value == null) return default(R);
			return block(value);
		}

		public static T Also<T>(this T value, Func<T,T> block)
		{
			if (value == null) return default(T);
			return block(value);
		}

		public static RectangleF Scale(this Rectangle rectangle, float scaleBy)
		{
			return new RectangleF(rectangle.X * scaleBy, rectangle.Y * scaleBy, rectangle.Width * scaleBy, rectangle.Height * scaleBy);
		}
	}
}
