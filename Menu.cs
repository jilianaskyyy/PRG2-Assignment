
//==========================================================
// Student Number : S10271091G
// Student Name : Capili Jiliana Sky Almonte
//==========================================================

using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10272091_PRG2Assignment
{
    internal class Menu
    {
        private string menuId;
        private string menuName;
        private FoodItem fooditem;
        private List<FoodItem> fooditems;

        public string MenuId { get; set; }
        public string MenuName { get; set; }
        public FoodItem FoodItem { get; set; }
        public List<FoodItem> FoodItems { get; set; }

        public Menu() {
            FoodItems = new List<FoodItem>();

        }

        public Menu(string menuId, string menuName)
        {
            MenuId = menuId;
            MenuName = menuName;
            FoodItems = new List<FoodItem>();
        }

        public void AddFoodItem(FoodItem fooditem)
        {
            FoodItems.Add(fooditem);

        }

        public bool RemoveFoodItem(FoodItem fooditem)
        {
            if (FoodItems.Contains(fooditem))
            {
                FoodItems.Remove(fooditem);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DisplayFoodItems()
        {
            int counter = 0;
            foreach (FoodItem item in FoodItems)
            {
                counter++;
                Console.WriteLine($"{counter}. {item.ItemName} - ${item.ItemPrice}");
            }
        }
        
        public string ToString()
        {
            return $"{MenuId,-10}{MenuName,-10}";
        }
    }
}
