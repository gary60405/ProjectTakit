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
    using Google.Cloud.Firestore;
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
            //OrderRepository OrderRepository = new OrderRepository();
            //Task DataTask = OrderRepository.GetFirebaseData();
            //FirestoreDb db = FirestoreDb.Create("takit-29fe4");
            //DocumentReference document = db.Document("users/1");
            //FirestoreChangeListener listener = document.Listen(snapshot =>
            //{
            //    if (snapshot.Exists)
            //    {
            //        StoreName = "小畇成";
            //    }
            //    StoreName = "畇成";
            //});
            LoginName = "Gary";
            StoreName = "小畇成";
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

        public void OnNavigatedTo(INavigationParameters parameters)
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
            else if(parameters.GetValue<string>("Mode") == "Update")
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
