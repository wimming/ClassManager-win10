using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace ClassManager.Controls
{
	/// <summary>
	/// Translates true values to Visibility.Visible and false values to
	/// Visibility.Collapsed, or the reverse if the parameter is "Reverse".
	/// </summary>
	public class BooleanToVisibilityConverter : IValueConverter
	{
		public object Convert (object value, Type targetType, object parameter, string language)
		{
			if ((bool)value) {
				return Visibility.Visible;
			} else {
				return Visibility.Collapsed;
			}
		}

		public object ConvertBack (object value, Type targetType, object parameter, string language)
		{
			return (Visibility)value == Visibility.Visible;
		}
			
	}

	public class InverseBooleanToVisibilityConverter : IValueConverter
	{
		public object Convert (object value, Type targetType, object parameter, string language)
		{
			if (!(bool)value) {
				return Visibility.Visible;
			} else {
				return Visibility.Collapsed;
			}
		}

		public object ConvertBack (object value, Type targetType, object parameter, string language)
		{
			return (Visibility)value != Visibility.Visible;
		}

	}

	public class BoolConver : IValueConverter
	{
		// Define the Convert method to convert a DateTime value to 
		// a month string.
		public object Convert (object value, Type targetType,
			object parameter, string language)
		{
			// value is the data from the source object.
			Boolean t = (Boolean)value;
			// Return the value to pass to the target.
			return t;
		}

		// ConvertBack is not implemented for a OneWay binding.
		public object ConvertBack (object value, Type targetType,
			object parameter, string language)
		{
			Boolean t = (Boolean)value;
			// Return the value to pass to the target.
			return t;
		}
	}
}
