using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ProjectTakit.Models
{
    public enum OrderState { Uncheck, Making,  Finished, Shipping, Canceled};
    public class OrderStateSet : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string StateText { get; set; }
        public int StateNum { get; set; }
    }
    public class CorporateInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public List<OrderForm> OrderForms { get; set; }
        public bool IsClose { get; set; }
        public OpenDate OpenDate { get; set; }
    }
    public class OpenDate : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public List<OpenTimes> Mon { get; set; }
        public List<OpenTimes> Tue { get; set; }
        public List<OpenTimes> Wen { get; set; }
        public List<OpenTimes> Thr { get; set; }
        public List<OpenTimes> Fri { get; set; }
        public List<OpenTimes> Sat { get; set; }
        public List<OpenTimes> Sun { get; set; }
    }
    public class OpenTimes : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //hr,min
        public string Start { get; set; }
        public string End { get; set; }
}
    public class OrderForm : INotifyPropertyChanged, ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string OrderId { get; set; }
        public DateTime CreateTime { get; set; }
        public int OrderStatus { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public int ToatalPrice { get; set; }
        public string UserId { get; set; }

        public object Clone()
        {
            var fooObject = ((ICloneable)this).Clone();
            return fooObject as OrderForm;
        }
        object ICloneable.Clone()
        {
            return this.MemberwiseClone();
        }
    }
    public class OrderItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }
        public List<AddOns> AddOns { get; set; }
        public List<Flavor> Flavors { get; set; }
        public string Notes { get; set; }
    }
    public class Flavor : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Name { get; set; }
        public string Selected { get; set; }
        public List<string> Degree { get; set; }
    }
    public class AddOns : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public int MaxAmount { get; set; }
    }
}
