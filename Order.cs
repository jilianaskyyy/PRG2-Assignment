
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
    internal class Order
    {
        private int orderId;
        private DateTime orderDateTime;
        private double orderTotal;
        private string orderStatus;
        private DateTime deliveryDateTime;
        private string deliveryAddress;
        private string orderPaymentMethod;
        private bool orderPaid;
        private OrderedFoodItem orderedfooditem;
        private List<OrderedFoodItem> orderedfooditems;
        private SpecialOffer specialoffer;
  

        public int OrderId { get; set; }
        public DateTime OrderDateTime { get; set; }
        public double OrderTotal { get; set; }
        public string OrderStatus { get; set; }
        public DateTime DeliveryDateTime { get; set; }
        public string DeliveryAddress { get; set; }
        public string OrderPaymentMethod { get; set; }
        public bool OrderPaid { get; set; }
        public OrderedFoodItem Orderedfooditem { get; set; }
        public List<OrderedFoodItem> OrderedFoodItems { get; set; }
        public SpecialOffer SpecialOffer { get; set; }


        public Order() { }

        public Order(int orderId, DateTime orderDateTime, double orderTotal, string orderStatus, DateTime deliveryDateTime, string deliveryAddress, string orderPaymentMethod, bool orderPaid)
        {
            OrderId = orderId;
            OrderDateTime = orderDateTime;
            OrderTotal = orderTotal;
            OrderStatus = orderStatus;
            DeliveryDateTime = deliveryDateTime;
            DeliveryAddress = deliveryAddress;
            OrderPaymentMethod = orderPaymentMethod;
            OrderPaid = orderPaid;
            OrderedFoodItems = new List<OrderedFoodItem>();
            
        }

        public double CalculateOrderTotal()
        {
            OrderTotal = 0;
            foreach(OrderedFoodItem item in orderedfooditems)
            {
                OrderTotal += item.CalculateSubtotal();
            }
            return OrderTotal;
        }

        public void AddOrderedFoodItem(OrderedFoodItem item)
        {
            OrderedFoodItems.Add(item);
        }

        public bool RemoveOrderedFoodItem(OrderedFoodItem item)
        {
            if (OrderedFoodItems.Contains(item))
            {
                OrderedFoodItems.Remove(item);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DisplayOrderedFoodItems()
        {
            int counter = 0;
            foreach (OrderedFoodItem item in orderedfooditems)
            {
                Console.WriteLine($"{counter++}. {item.ItemName} - {item.QtyOrdered}");
            }
            
        }

        public string ToString()
        {
            return$"{OrderId,-10}{OrderDateTime,-10}{OrderTotal,-10}{OrderStatus,-10}{DeliveryDateTime,-10}{DeliveryAddress,-10}{OrderPaymentMethod,-10}{OrderPaid,-10}";
        }
    }
}
