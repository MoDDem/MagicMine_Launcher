using System;
using System.Globalization;
using System.Windows.Data;

namespace MagicMine_Launcher.Components {
	[ValueConversion(typeof(Enum), typeof(bool))]
	class IsEnumEqualsParameterConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
			parameter == null || value == null ? false : (object) ((int) parameter == (int) value);

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 
			(bool) value ? parameter : Enum.GetValues(parameter.GetType()).GetValue(0);
	}
}
