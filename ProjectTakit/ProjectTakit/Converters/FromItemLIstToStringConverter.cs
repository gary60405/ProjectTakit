using ProjectTakit.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ProjectTakit.Converters
{
    class FromItemLIstToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string OutputText = "";
            switch (parameter.ToString())
            {
                case "NAME":
                    if (((List<AddOns>)value)[0].Name == "null")
                    {
                        return null;
                    }
                    else
                    {
                        foreach (var AddOn in (List<AddOns>)value)
                        {
                            OutputText += $"(+){AddOn.Name}\n";
                        }
                        return OutputText;
                    }
                case "AMOUNT":
                    if (((List<AddOns>)value)[0].Name == "null")
                    {
                        return null;
                    }
                    else
                    {
                        foreach (var AddOn in (List<AddOns>)value)
                        {
                            OutputText += $"{AddOn.Amount}\n";
                        }
                        return OutputText;
                    }
                case "PRICE":
                    if (((List<AddOns>)value)[0].Name == "null")
                    {
                        return null;
                    }
                    else
                    {
                        foreach (var AddOn in (List<AddOns>)value)
                        {
                            OutputText += $"{AddOn.Price}\n";
                        }
                        return OutputText;
                    }
                case "ADDON_HEIGHT":
                    if (((List<AddOns>)value)[0].Name == "null")
                    {
                        return 0;
                    }
                    var AddOnsList = (List<AddOns>)value;
                    return AddOnsList.Count * 30;
                case "FLAVOR_HEIGHT":
                    if (((List<Flavor>)value)[0].Name == "null")
                    {
                        return 0;
                    }
                    var FlavorList = (List<Flavor>)value;
                    return FlavorList.Count * 30;
                case "FLAVORS":
                    if (((List<Flavor>)value)[0].Name == "null")
                    {
                        return null;
                    }
                    else
                    {
                        foreach (var flavor in (List<Flavor>)value)
                        {
                            OutputText += $"{flavor.Name}：{flavor.Selected}\n";
                        }
                        return OutputText;
                    }
                default:
                    return OutputText;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
