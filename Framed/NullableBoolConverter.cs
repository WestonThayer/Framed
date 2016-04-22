﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Framed
{
    public sealed class NullableBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!value.GetType().Equals(typeof(bool)))
            {
                throw new ArgumentException("Only Boolean is supported");
            }

            if (targetType.Equals(typeof(bool?)))
            {
                return value as bool?;
            }
            else
            {
                throw new ArgumentException("Unsuported type {0}", targetType.FullName);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (targetType.Equals(typeof(bool)))
            {
                return value ?? false;
            }
            else
            {
                throw new ArgumentException("Unsuported type {0}", targetType.FullName);
            }
        }
    }
}
