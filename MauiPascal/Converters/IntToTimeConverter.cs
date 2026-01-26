using System.Globalization;

namespace MauiPascal.Converters;

public class IntToTimeConverter : IValueConverter
{
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if(value is int seconds)
		{
			var time = TimeSpan.FromSeconds(seconds);
			return time.ToString(@"hh\:mm");
		}
		return string.Empty;
	}

	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}