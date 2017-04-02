using System;
using System.Globalization;
using System.Windows.Data;
using Palmmedia.WpfGraph.Core;

namespace Palmmedia.WpfGraph.UI.Resources
{
    /// <summary>
    /// Converts an <see cref="EdgeDirection"/> to an <see cref="int"/> and vise versa.
    /// </summary>
    public class EdgeDirection2IntConverter : IValueConverter
    {
        /// <summary>
        /// Converts an <see cref="EdgeDirection"/>  to a <see cref="int"/>.
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
            return (int)value;
        }

        /// <summary>
        /// Converts a <see cref="int"/> to an <see cref="EdgeDirection"/>.
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
            return (EdgeDirection)value;
        }
    }
}
