
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
    internal class OrderedFoodItem : FoodItem
    {
        private int qtyOrdered;
        private double subTotal;

        public int QtyOrdered { get; set; }
        public double SubTotal { get; set; }

        public OrderedFoodItem():base() { }

        public OrderedFoodItem(string itemName, string itemDesc, double itemPrice, string customise,int qtyOrdered, double subTotal): base(itemName,itemDesc,itemPrice,customise)
        {
            ItemName = itemName;
            ItemDesc = itemDesc;
            ItemPrice = itemPrice;
            Customise = customise;
            QtyOrdered = qtyOrdered;
            SubTotal = subTotal;

        }

        public double CalculateSubtotal()
        {
            return QtyOrdered * ItemPrice;
        }
    }
}

