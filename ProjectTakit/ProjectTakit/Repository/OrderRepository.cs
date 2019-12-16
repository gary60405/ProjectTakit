using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjectTakit.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTakit.Repository
{
    public class OrderRepository
    {
        public List<OrderForm> GetOrderForms()
        {
            List<OrderForm> OrderForms = new List<OrderForm>();
            for (int i = 0; i < 10; i++)
            {
                var Items = GetOrderItems();
                var Price = 0;
                var rand = new Random();
                Items.ForEach((item) =>
                {
                    var AddOnPrice = 0;
                    item.AddOns.ForEach((AddOn) =>
                    {
                        AddOnPrice += AddOn.Price * AddOn.Amount;
                    });
                    Price += item.Price * item.Amount + AddOnPrice;
                });
                OrderForms.Add(new OrderForm
                {
                    OrderId = $"A0000{i}",
                    CreateTime = DateTime.Now.AddDays(i),
                    OrderStatus = rand.Next(0, 5),
                    TotalPrice = Price,
                    OrderItems = Items,
                    UserId = "0911123456"
                });
            }
            return OrderForms;
        }
        public List<OrderItem> GetOrderItems()
        {
            var rand = new Random();
            string JsonText = "[ { \"ItemId\": 1, \"ItemName\": \"翡翠綠茶\", \"Price\": 25, }, { \"ItemId\": 2, \"ItemName\": \"白毫綠茶\", \"Price\": 30, }, { \"ItemId\": 3, \"ItemName\": \"龍眼蜜茶\", \"Price\": 45, }, { \"ItemId\": 4, \"ItemName\": \"復刻醇奶\", \"Price\": 55, }, { \"ItemId\": 5, \"ItemName\": \"檸檬紅茶\", \"Price\": 45, }, { \"ItemId\": 6, \"ItemName\": \"梅子酵素\", \"Price\": 75, }, { \"ItemId\": 7, \"ItemName\": \"黑糖冬瓜\", \"Price\": 25, }, { \"ItemId\": 8, \"ItemName\": \"仕女紅茶\", \"Price\": 30, }, { \"ItemId\": 9, \"ItemName\": \"天香紅茶\", \"Price\": 30, }, { \"ItemId\": 10, \"ItemName\": \"烏巴紅茶\", \"Price\": 35, } ]";
            List<OrderItem> DummyData = JsonConvert.DeserializeObject<List<OrderItem>>(JsonText);
            List<OrderItem> OrderItems = new List<OrderItem>();
            for (int i = 0; i < 5; i++)
            {
                var TempItem = DummyData[rand.Next(0, 10)];
                OrderItems.Add(new OrderItem
                {
                    ItemId = TempItem.ItemId,
                    ItemName = TempItem.ItemName,
                    Amount = rand.Next(1, 5),
                    Price = TempItem.Price,
                    AddOns = GetAddOns(),
                    Flavors = GetFlavors(),
                    Notes = $"Notes{i}"
                });
            }
            return OrderItems;
        }
        public List<Flavor> GetFlavors()
        {
            var rand = new Random();
            string FlavorJsonText = "[ { \"Name\": \"甜度\", \"Selected\": \"微糖\", \"Degree\": [\"無糖\",\"微糖\",\"少糖\",\"正常\",], }, { \"Name\": \"冰塊\", \"Selected\": \"微冰\", \"Degree\": [\"去冰\",\"微冰\",\"少冰\",\"正常\",], } ]";
            List<Flavor> FlavorDummyData = JsonConvert.DeserializeObject<List<Flavor>>(FlavorJsonText);
            List<Flavor> Flavors = new List<Flavor>();
            foreach (var Flavor in FlavorDummyData)
            {
                Flavors.Add(new Flavor
                {
                    Name = Flavor.Name,
                    Selected = Flavor.Degree[rand.Next(0, 4)],
                    Degree = Flavor.Degree,
                });
            }
            return Flavors;
        }
        public List<AddOns> GetAddOns()
        {
            var rand = new Random();
            var AddOnsJsonText = "[ { \"Name\": \"椰果\", \"Price\": 10, }, { \"Name\": \"白玉珍珠\", \"Price\": 10, }, { \"Name\": \"奇亞子\", \"Price\": 15, }]";
            List<AddOns> AddOnsDummyData = JsonConvert.DeserializeObject<List<AddOns>>(AddOnsJsonText);
            List<AddOns> AddOns = new List<AddOns>();
            AddOns tempAddOn = AddOnsDummyData[rand.Next(0, 3)];
            AddOns.Add(new AddOns
            {
                Name = tempAddOn.Name,
                Price = tempAddOn.Price,
                Amount = 1
            });
            return AddOns;
        }
    }
}