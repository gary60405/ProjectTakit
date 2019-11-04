using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectTakit.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using Google.Cloud.Firestore;
    using Prism.Events;
    using Prism.Navigation;
    using Prism.Services;
    using ProjectTakit.Models;
    using Xamarin.Forms;

    public class OrderStatePageViewModel : INotifyPropertyChanged, INavigationAware
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly INavigationService navigationService;

        public OrderForm OrderFormSelected { get; set; }
        public int StatePikerSelected { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand PickerSelectedChangeCommand { get; set; }
        public ObservableCollection<OrderItem> OrderItems { get; set; } = new ObservableCollection<OrderItem>();
        public ObservableCollection<int> StatePickerList { get; set; } = new ObservableCollection<int>();
        public OrderStatePageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            StatePickerList.Add((int)OrderState.Uncheck);
            StatePickerList.Add((int)OrderState.Making);
            StatePickerList.Add((int)OrderState.Finished);
            StatePickerList.Add((int)OrderState.Shipping);
            StatePickerList.Add((int)OrderState.Canceled);
            SaveCommand = new DelegateCommand(() => {
                NavigationParameters fooPara = new NavigationParameters
                {
                    { "OrderForm", OrderFormSelected },
                    { "Mode", "Update" }
                };
                navigationService.GoBackAsync(fooPara);
            });
            PickerSelectedChangeCommand = new DelegateCommand(() => { 
            });
        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if(parameters.ContainsKey("OrderForm"))
            {
                OrderFormSelected = parameters.GetValue<OrderForm>("OrderForm");
                foreach (var OrderItem in OrderFormSelected.OrderItems)
                {
                    OrderItems.Add(new OrderItem
                    {
                        ItemId = OrderItem.ItemId,
                        ItemName = OrderItem.ItemName,
                        Amount = OrderItem.Amount,
                        Price = OrderItem.Price,
                        Notes = OrderItem.Notes,
                        Flavors = OrderItem.Flavors,
                        AddOns = OrderItem.AddOns
                    });
                }
            }
        }

        public void OnNavigatingTo(INavigationParameters parameters)
        {
        }

    }
}
