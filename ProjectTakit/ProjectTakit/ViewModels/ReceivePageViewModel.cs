using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectTakit.ViewModels
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Prism.Events;
    using Prism.Navigation;
    using Prism.Services;
    using ProjectTakit.Models;
    using ProjectTakit.Repository;

    public class ReceivePageViewModel : INotifyPropertyChanged, INavigationAware
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly INavigationService navigationService;
        public string LoginName { get; set; }
        public string StoreName { get; set; }
        public OrderForm OrderFormSelected { get; set; }
        public bool RefreshingStatus { get; set; }
        public ObservableCollection<OrderForm> OrderFormList { get; set; } = new ObservableCollection<OrderForm>();
        public int RefreshIndex { get; set; } = 0;
        public DelegateCommand OrderFormSelectedCommand { get; set; }
        public DelegateCommand OrderFormRefreshCommand { get; set; }


        public ReceivePageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            LoginName = "Gary";
            StoreName = "一壺青";
            OrderFormSelectedCommand = new DelegateCommand(async () =>
            {
                NavigationParameters fooPara = new NavigationParameters
                {
                    { "OrderForm", OrderFormSelected.Clone() }
                };
                await navigationService.NavigateAsync("OrderStatePage", fooPara);
            });
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.GetNavigationMode() == NavigationMode.New)
            {
                OrderRepository OrderRepository = new OrderRepository();
                var OrderForms = OrderRepository.GetOrderForms();
                foreach (var OrderForm in OrderForms)
                {
                    OrderFormList.Add(new OrderForm
                    {
                        OrderId = OrderForm.OrderId,
                        CreateTime = OrderForm.CreateTime,
                        OrderStatus = OrderForm.OrderStatus,
                        ToatalPrice = OrderForm.ToatalPrice,
                        OrderItems = OrderForm.OrderItems,
                        UserId = OrderForm.UserId,
                    });
                }
            }
            else if (parameters.GetValue<string>("Mode") == "Update")
            {
                var OrderFormSelected = parameters.GetValue<OrderForm>("OrderForm");
                var index = OrderFormList.IndexOf(OrderFormList.FirstOrDefault(x => x.OrderId == OrderFormSelected.OrderId));
                OrderFormList[index].OrderStatus = OrderFormSelected.OrderStatus;
            }
        }

        public void OnNavigatingTo(INavigationParameters parameters)
        {
        }

    }
}
