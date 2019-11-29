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
        public string Output { get; set; }
        public List<string> OrderDataIdList { get; set; }
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
            //OrderFormRefreshCommand = new DelegateCommand(async () => 
            //{
            //    //OrderFormList.
            //});
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
                //OrderRepository OrderRepository = new OrderRepository();
                //var OrderForms = OrderRepository.GetOrderForms();
                //foreach (var OrderForm in OrderForms)
                //{
                //    OrderFormList.Add(new OrderForm
                //    {
                //        OrderId = OrderForm.OrderId,
                //        CreateTime = OrderForm.CreateTime,
                //        OrderStatus = OrderForm.OrderStatus,
                //        ToatalPrice = OrderForm.ToatalPrice,
                //        OrderItems = OrderForm.OrderItems,
                //        UserId = OrderForm.UserId,
                //    });
                //}
                var client = new FirebaseClient("https://takit-29fe4.firebaseio.com/");
                var OrderForms = await client.Child("testData").OnceAsync<OrderForm>();
                OrderDataIdList = new List<string>();
                foreach (var OrderForm in OrderForms)
                {
                    OrderDataIdList.Add(OrderForm.Key);
                    OrderFormList.Add(new OrderForm
                    {
                        OrderId = OrderForm.Object.OrderId,
                        CreateTime = OrderForm.Object.CreateTime,
                        OrderStatus = OrderForm.Object.OrderStatus,
                        ToatalPrice = OrderForm.Object.ToatalPrice,
                        OrderItems = OrderForm.Object.OrderItems,
                        UserId = OrderForm.Object.UserId,
                    });
                }

                var child = client
                        .Child("testData")
                        .AsObservable<OrderForm>()
                        .Subscribe(data =>
                        {
                            var OrderDataIndex = OrderDataIdList.FindIndex(key => data.Key == key);
                            if (OrderDataIndex != -1)
                            {
                                OrderFormList[OrderDataIndex] = new OrderForm
                                {
                                    OrderId = data.Object.OrderId,
                                    CreateTime = data.Object.CreateTime,
                                    OrderStatus = data.Object.OrderStatus,
                                    ToatalPrice = data.Object.ToatalPrice,
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
                                    ToatalPrice = data.Object.ToatalPrice,
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
                var client = new FirebaseClient("https://takit-29fe4.firebaseio.com/");
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
