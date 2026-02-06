
//==========================================================
// Student Number : S10271091G
// Student Name : Capili Jiliana Sky Almonte
//==========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10272091_PRG2Assignment
{
    internal class Restaurant
    {
        private string restaurantId;
        private string restaurantName;
        private string restaurantEmail;
        private Menu menu;
        private List<Menu> menuList;
        private SpecialOffer offer;
        private List<SpecialOffer> specialOfferList;
        private Order? order;
        private List<Order?> orders;

        public string RestaurantId { get; set; }
        public string RestaurantName {  get; set; }
        public string RestaurantEmail { get; set; }
        public Menu Menu { get; set; }
        public List<Menu> MenuList { get; set; }
        public SpecialOffer SpecialOffer { get; set; }
        public List<SpecialOffer> SpecialOfferList { get; set; }
        public Order? Order { get; set; }
        public List<Order?> OrderList { get; set; }



        

        public Restaurant() {
            MenuList = new List<Menu>();
            SpecialOfferList = new List<SpecialOffer>();
            OrderList = new List<Order?>();
        }

        public Restaurant(string restaurantId, string restaurantName, string restaurantEmail)
        {
            RestaurantId = restaurantId;
            RestaurantName = restaurantName;
            RestaurantEmail = restaurantEmail;
            MenuList = new List<Menu>();
            SpecialOfferList = new List<SpecialOffer>();
            OrderList = new List<Order?>();
            
        }

        public void DisplayOrders()
        {
            foreach (Order o in OrderList)
            {
                Console.WriteLine($"{o.OrderId}");
            }
        }

        public void DisplaySpecialOffers()
        {
            foreach (SpecialOffer s in SpecialOfferList)
            {
                Console.WriteLine($"{s.ToString()}");
            }
        }

        public void DisplayMenu()
        {
            foreach (Menu m in MenuList)
            {
                m.DisplayFoodItems();
            }
        }

        public void AddMenu(Menu menu)
        {
            MenuList.Add(menu);
        }

        public bool RemoveMenu(Menu menu)
        {
            if (MenuList.Contains(menu))
            {
                MenuList.Remove(menu);
                return true;
            }
            else
            {
                return false;
            }
        }

        public string ToString()
        {
            return $"Restaurant: {RestaurantName}  ({RestaurantId})";
        }

    }
}
