using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Framed.Extensions;

namespace Framed
{
    public sealed class ColorToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!value.GetType().Equals(typeof(Color)))
            {
                throw new ArgumentException("Only Color is supported");
            }

            if (targetType.Equals(typeof(string)))
            {
                return (value as Color? ?? Color.FromArgb(0, 0, 0, 0)).ToString();
            }
            else
            {
                throw new ArgumentException("Unsuported type {0}", targetType.FullName);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (targetType.Equals(typeof(Color)))
            {
                return ColorExtensions.Parse(value as string);
            }
            else
            {
                throw new ArgumentException("Unsuported type {0}", targetType.FullName);
            }
        }
    }
}
