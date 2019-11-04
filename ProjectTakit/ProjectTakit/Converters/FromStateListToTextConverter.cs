using ProjectTakit.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ProjectTakit.Converters
{
    class FromStateListToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IList<string> TempList = new List<string>();
            foreach (var item in value as IList<int>)
            {
                if (item == (int)OrderState.Uncheck)
                {
                    TempList.Add("待確認");
                }
                else if (item == (int)OrderState.Making)
                {
                    TempList.Add("製作中");
                }
                else if (item == (int)OrderState.Finished)
                {
                    TempList.Add("已完成");
                }
                else if (item == (int)OrderState.Shipping)
                {
                    TempList.Add("運送中");
                }
                else if (item == (int)OrderState.Canceled)
                {
                    TempList.Add("已取消");
                }
            }
            return TempList as IList<string>;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
