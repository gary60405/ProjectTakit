using ProjectTakit.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ProjectTakit.Converters
{
    class FormStateToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((int)value)
            {
                case((int)OrderState.Uncheck):
                    return Color.Blue;
                case ((int)OrderState.Making):
                    return Color.Red;
                case ((int)OrderState.Finished):
                    return Color.Green;
                case ((int)OrderState.Shipping):
                    return Color.Orange;
                case ((int)OrderState.Canceled):
                    return Color.Gray;
                default:
                    return Color.Gray;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
