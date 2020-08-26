﻿using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace TNTDrawing.Converters
{
	/// <summary>
	/// Converts <see cref="Grid"/> into object that can be viewed in <see cref="PropertyGrid"/> as a 
	/// property of another object
	/// </summary>
	public class GridTypeConverter : TypeConverter
	{
		/// <summary>
		/// Returns the width and height values that are displayed in <see cref="PropertyGrid"/> at the object level
		/// </summary>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(String))
			{
				var grid = value as Grid;
				return $"{grid.Width}, {grid.Height}";
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>
		/// Gets a listing of <see cref="Grid"/> properties that can be viewed by the <see cref="PropertyGrid"/>
		/// </summary>
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(typeof(Grid), attributes);//.Sort(new string[] { "PixelPerSegment", "LineColor", "BackgroundColor" });
		}

		/// <summary>
		/// Allows all <see cref="Grid"/> properties to be listed in <see cref="PropertyGrid"/>
		/// </summary>
		public override bool GetPropertiesSupported(ITypeDescriptorContext context) => true;
	}
}
