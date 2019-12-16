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
    using Firebase.Database;
    using Firebase.Database.Query;
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
        public ObservableCollection<OrderForm> OrderFormList { get; set; } = new ObservableCollection<OrderForm>();
        public DelegateCommand OrderFormSelectedCommand { get; set; }


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

            FirebaseClient client = new FirebaseClient("https://takit-29fe4.firebaseio.com/");
            if (parameters.GetNavigationMode() == NavigationMode.New)
            {
                //OrderRepository OrderRepository = new OrderRepository();
                //var OrderForms = OrderRepository.GetOrderForms();
                var OrderForms = await client.Child("testData").OnceAsync<OrderForm>();
                foreach (var OrderForm in OrderForms)
                {
                    OrderFormList.Add(new OrderForm
                    {
                        OrderId = OrderForm.Object.OrderId,
                        CreateTime = OrderForm.Object.CreateTime,
                        OrderStatus = OrderForm.Object.OrderStatus,
                        TotalPrice = OrderForm.Object.TotalPrice,
                        OrderItems = OrderForm.Object.OrderItems,
                        UserId = OrderForm.Object.UserId,
                    });
                }
                //OrderFormList.Sort((x, y) => { return -x.CreateTime.CompareTo(y.CreateTime); });
                var child = client
                        .Child("testData")
                        .AsObservable<OrderForm>()
                        .Subscribe(data =>
                        {
                            var OrderDataIndex = OrderFormList.IndexOf(OrderFormList.FirstOrDefault(x => x.OrderId == data.Object.OrderId));
                            if (OrderDataIndex != -1)
                            {
                                OrderFormList[OrderDataIndex] = new OrderForm
                                {
                                    OrderId = data.Object.OrderId,
                                    CreateTime = data.Object.CreateTime,
                                    OrderStatus = data.Object.OrderStatus,
                                    TotalPrice = data.Object.TotalPrice,
                                    OrderItems = data.Object.OrderItems,
                                    UserId = data.Object.UserId,
                                };
                            }
                            else
                            {
                                OrderFormList.Add(new OrderForm
                                {
                                    OrderId = data.Object.OrderId,
                                    CreateTime = data.Object.CreateTime,
                                    OrderStatus = data.Object.OrderStatus,
                                    TotalPrice = data.Object.TotalPrice,
                                    OrderItems = data.Object.OrderItems,
                                    UserId = data.Object.UserId,
                                });
                            }
                        });
            }
            else if (parameters.GetValue<string>("Mode") == "Update")
            {
                var OrderFormSelected = parameters.GetValue<OrderForm>("OrderForm");
                var index = OrderFormList.IndexOf(OrderFormList.FirstOrDefault(x => x.OrderId == OrderFormSelected.OrderId));
                OrderFormList[index].OrderStatus = OrderFormSelected.OrderStatus;
                var child = client.Child("testData");
                var OrderFormKey = (await child.OnceAsync<OrderForm>()).Where(form => form.Object.OrderId == OrderFormSelected.OrderId).FirstOrDefault().Key;
                await child
                  .Child(OrderFormKey)
                  .PutAsync(OrderFormList[index]);

            }
        }

        public void OnNavigatingTo(INavigationParameters parameters)
        {
        }

    }
}
