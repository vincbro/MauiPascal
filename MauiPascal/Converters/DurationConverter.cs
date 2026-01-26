using System.Globalization;

namespace MauiPascal.Converters;

public class DurationConverter : IValueConverter
{
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if (value is not int minutes)
			return string.Empty;

		if (minutes == 0)
			return "0 min";

		int hours = minutes / 60;
		int remainingMinutes = minutes % 60;

		if (hours == 0)
			return $"{minutes} min";

		if (remainingMinutes == 0)
			return $"{hours}h";

		return $"{hours}h {remainingMinutes}min";
	}

	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
