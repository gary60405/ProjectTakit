using ProjectTakit.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ProjectTakit.Converters
{
    class FormStateToStateTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((int)value)
            {
                case ((int)OrderState.Uncheck):
                    return "待確認";
                case ((int)OrderState.Making):
                    return "製作中";
                case ((int)OrderState.Finished):
                    return "已完成";
                case ((int)OrderState.Shipping):
                    return "運送中";
                case ((int)OrderState.Canceled):
                    return "已取消";
                default:
                    return "Error";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
