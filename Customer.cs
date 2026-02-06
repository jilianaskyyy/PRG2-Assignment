
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
    internal class Customer
    {
        private string emailAddress;
        private string customerName;
        private Order order;
        private List<Order> orders;
        private string referralCode;
        private bool referralPromo;

        public string EmailAddress { get; set; }
        public string CustomerName { get; set; }
        public Order Order { get; set; }
        public List<Order> Orders { get; set; }
        public string ReferralCode {  get; set; }
        public bool ReferralPromo { get; set; }

        public Customer() {
            Orders = new List<Order>();
        }

        public Customer(string emailAddress, string customerName, bool referralPromo)
        {
            EmailAddress = emailAddress;
            CustomerName = customerName;
            Orders = new List<Order>();
            ReferralCode = customerName;
            ReferralPromo = referralPromo;
        }

        public void AddOrder(Order order)
        {
            Orders.Add(order);
        }

        public void DisplayAllOrders()
        {
            foreach (Order o in Orders)
            {
                Console.WriteLine($"{o.OrderId}");
            }
        }

        public bool RemoveOrder(Order order)
        {
            if (Orders.Contains(order))
            {
                Orders.Remove(order);
                return true;
            }
            else
            {
                return false;
            }
        }

        public string ToString()
        {
            return $"{CustomerName,-10}{EmailAddress,-10}";

        }
    }
}
