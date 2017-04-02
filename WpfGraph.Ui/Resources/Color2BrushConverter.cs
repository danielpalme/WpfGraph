using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Palmmedia.WpfGraph.UI.Resources
{
    /// <summary>
    /// Converts a <see cref="Color"/> to a <see cref="SolidColorBrush"/> and vise versa.
    /// </summary>
    public class Color2BrushConverter : IValueConverter
    {
        /// <summary>
        /// Converts a <see cref="Color"/>  to a <see cref="SolidColorBrush"/>.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string stringValue = value as string;

            if (stringValue != null && stringValue.Length == 0)
            {
                return null;
            }
            else
            {
                return new SolidColorBrush((Color)value);
            }
        }

        /// <summary>
        /// Converts a <see cref="SolidColorBrush"/> to a <see cref="Color"/>.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((SolidColorBrush)value).Color;
        }
    }
}
