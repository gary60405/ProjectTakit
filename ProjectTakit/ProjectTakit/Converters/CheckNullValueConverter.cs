using ProjectTakit.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ProjectTakit.Converters
{
    class CheckNullValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (parameter.ToString())
            {
                case "USERID":
                    if (value != null)
                    {
                        return value;
                    }
                    return "手機遺失";
                case "ORDERID":
                    if (value != null)
                    {
                        return value;
                    }
                    return "訂單編號遺失";
                case "PRICE":
                    if (value != null)
                    {
                        return value;
                    }
                    return "訂單金額遺失";
                case "DATE":
                    if (value != null)
                    {
                        return value;
                    }
                    return "訂單日期遺失";
                default:
                    return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
