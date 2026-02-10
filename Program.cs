
//==========================================================
// Student Number : S10271091G
// Student Name : Capili Jiliana Sky Almonte
//==========================================================
using S10272091_PRG2Assignment;
using System.Diagnostics.Metrics;
using System.Globalization;
using CsvHelper;

List<Restaurant> RestaurantList = new List<Restaurant>();
List<Customer>CustomerList = new List<Customer>();
List<Order>OrderList = new List<Order>();

//LOADING RESTAURANT.CSV
void LoadRestaurant()
{
    using (StreamReader sr = new StreamReader("C:/Users/Jiliana Sky/OneDrive - Ngee Ann Polytechnic/Desktop/1.2/PRG II/Week13/S10272091_PRG2Assignment/restaurants.csv"))
    {
        string? line;
        sr.ReadLine();
        while ((line = sr.ReadLine()) != null)
        {
            string[] parts = line.Split(',');
            Restaurant r = new Restaurant(parts[0], parts[1], parts[2]);
            RestaurantList.Add(r);

        }
    }
}
LoadRestaurant();

//LOADING FOODITEMS.CSV
void LoadFoodItems()
{
    using (StreamReader sr = new StreamReader("C:/Users/Jiliana Sky/OneDrive - Ngee Ann Polytechnic/Desktop/1.2/PRG II/Week13/S10272091_PRG2Assignment/fooditems.csv"))
    {
        List<FoodItem> FoodItemList = new List<FoodItem>();
        string? line;
        sr.ReadLine();
        while ((line = sr.ReadLine()) != null)
        {
            string[] parts = line.Split(',');
            foreach(Restaurant r in RestaurantList)
            {
                if(r.RestaurantId == parts[0])
                {
                    if(r.MenuList.Count <= 0)
                    {
                        r.AddMenu(new Menu());
                    }
                    FoodItem item = new FoodItem(parts[1], parts[2], Convert.ToDouble(parts[(parts.Length) -1]), null);
                    r.MenuList[0].AddFoodItem(item);
                }
            }

        

        }
    }
}
LoadFoodItems();

//LOADING CUSTOMER.CSV
void LoadCustomer()
{
    using (StreamReader sr = new StreamReader("C:/Users/Jiliana Sky/OneDrive - Ngee Ann Polytechnic/Desktop/1.2/PRG II/Week13/S10272091_PRG2Assignment/customers.csv"))
    {
        string? line;
        sr.ReadLine();
        while ((line = sr.ReadLine()) != null)
        {
            string[] parts = line.Split(',');
            Customer c = new Customer(parts[1], parts[0], false);
            CustomerList.Add(c);

        }
    }
}
LoadCustomer();

//LOADING ORDER.CSV
void LoadOrder()
{
    using StreamReader sr = new StreamReader("C:/Users/Jiliana Sky/OneDrive - Ngee Ann Polytechnic/Desktop/1.2/PRG II/Week13/S10272091_PRG2Assignment/orders.csv") ;
    using CsvReader csv = new CsvReader(sr, CultureInfo.InvariantCulture);
    csv.Read();
    csv.ReadHeader();
    while (csv.Read())
    {
       
         Order o = new Order(Convert.ToInt32(csv.GetField(0)), DateTime.Parse(csv.GetField(6)), Convert.ToDouble(csv.GetField(7)), csv.GetField(8), DateTime.Parse(csv.GetField(3) + " " + csv.GetField(4)), csv.GetField(5), null, false);
        string[] itemlist = csv.GetField(9).Split('|', StringSplitOptions.TrimEntries);
        var count = 0;
        foreach (string item in itemlist)
        {
            count++;
 
            string[] items = item.Split(',');
            OrderedFoodItem ordered = new OrderedFoodItem();
            ordered.ItemName = items[0];

            try
            {
                ordered.QtyOrdered = Convert.ToInt32(items[1]);
            }
            catch (Exception)
            {
                ordered.QtyOrdered = 0;
            }

            o.AddOrderedFoodItem(ordered);
     

        }
        foreach (Restaurant r in RestaurantList)
        {
            if (r.RestaurantId == csv.GetField(2))
            {
                r.OrderList.Add(o);
            }
        }

        foreach (Customer c in CustomerList)
        {
            if (c.EmailAddress == csv.GetField(1))
            {
                c.Orders.Add(o);
            }
        }

        OrderList.Add(o);
    }
}
LoadOrder();

//LOADING MENU ITEMS
void LoadMenuItems()
{
    Console.WriteLine("All Restaurants and Menu Items");
    Console.WriteLine("==============================");
    foreach (Restaurant r in RestaurantList)
    {
        Console.WriteLine(r.ToString());
        if (r.MenuList.Count > 0)
        {
            List<FoodItem> fooditems = (r.MenuList[0]).FoodItems;
            foreach (FoodItem item in fooditems)
            {
                Console.WriteLine(item.ToString());
            }
        }

    }

}

//DISPLAYING ALL ORDERS
void DisplayOrder()
{
    Console.WriteLine("All Orders");
    Console.WriteLine("==========");
    Console.WriteLine($"{"OrderID",-10} {"Customer",-20} {"Restaurant",-20} {"Delivery Date/Time",-25}{"Amount",-10} {"Status",-10}");
    Console.WriteLine($"{"--------",-10} {"----------",-20} {"-------------",-20} {"------------------",-25}{"------",-10} {"---------",-10}");
    foreach(Order o in OrderList)
    {
        Console.WriteLine($"{o.OrderId,-11}{GetCustomer(o.OrderId),-21}{GetRestaurant(o.OrderId),-21}{o.DeliveryDateTime.ToString("dd/MM/yyyy HH:mm"),-25}${o.OrderTotal,-10:F2}{o.OrderStatus,-10}");

    }

}

//FIND CUSTOMER WHO PLACED A SPECIFIC ORDER BASED ON THE ORDER ID
string GetCustomer(int OrderId)
{
    foreach(Customer c in CustomerList)
    {
        if(c.Orders.Count > 0)
        {
            foreach (Order o in c.Orders)
            {
                if(o.OrderId == OrderId)
                {
                    return c.CustomerName;
                }
            }
        }
    }
    return "";
}

//FIND RESTAURANT WHO PLACED A SPECIFIC ORDER BASED ON THE ORDER ID

string GetRestaurant(int OrderId)
{
    foreach (Restaurant r in RestaurantList)
    {
        if (r.OrderList.Count > 0)
        {
            foreach (Order o in r.OrderList)
            {
                if (o.OrderId == OrderId)
                {
                    return r.RestaurantName;
                }
            }
        }
    }
    return "";
}

//CREATE ORDER
void CreateOrder()
{
    Console.WriteLine("Create New Order");
    Console.WriteLine("================");

    //Get customer email and validate
    Customer customer = null;
    while (true)
    {
        Console.Write("Enter Customer Email: ");
        string email = Console.ReadLine()?.Trim();
        if (string.IsNullOrEmpty(email))
        {
            Console.WriteLine("Email cannot be empty.");
            continue;
        }

        customer = CustomerList.FirstOrDefault(c => c.EmailAddress.Equals(email, StringComparison.OrdinalIgnoreCase));
        if (customer == null)
        {
            Console.WriteLine("Customer not found. Please enter a valid email.");
        }
        else break;
    }

    //Get restaurant ID and validate
    Restaurant restaurant = null;
    while (true)
    {
        Console.Write("Enter Restaurant ID: ");
        string restaurantId = Console.ReadLine()?.Trim();
        if (string.IsNullOrEmpty(restaurantId))
        {
            Console.WriteLine("Restaurant ID cannot be empty.");
            continue;
        }

        restaurant = RestaurantList.FirstOrDefault(r => r.RestaurantId.Equals(restaurantId, StringComparison.OrdinalIgnoreCase));
        if (restaurant == null)
        {
            Console.WriteLine("Restaurant not found. Please enter a valid Restaurant ID.");
        }
        else break;
    }

    //Get delivery date, time, and validate
    DateTime deliveryDateTime;

    while (true)
    {
        Console.Write("Enter Delivery Date (dd/MM/yyyy): ");
        string date = Console.ReadLine()?.Trim();

        Console.Write("Enter Delivery Time (HH:mm or hh:mm tt): ");
        string time = Console.ReadLine()?.Trim();

        if (string.IsNullOrEmpty(date) || string.IsNullOrEmpty(time))
        {
            Console.WriteLine("Date and time cannot be empty.");
            continue;
        }

        string dateTimeInput = date + " " + time;

        string[] formats =
        {
        "dd/MM/yyyy HH:mm",
        "dd/MM/yyyy hh:mm tt"
    };

        if (!DateTime.TryParseExact(
                dateTimeInput,
                formats,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out deliveryDateTime))
        {
            Console.WriteLine("Invalid date/time format. Please try again.");
        }
        else if (deliveryDateTime < DateTime.Now)
        {
            Console.WriteLine("Delivery date/time cannot be in the past.");
        }
        else
        {
            break;
        }
    }

    //Get delivery address and validate
    string address;
    while (true)
    {
        Console.Write("Enter Delivery Address: ");
        address = Console.ReadLine()?.Trim();
        if (string.IsNullOrEmpty(address))
        {
            Console.WriteLine("Address cannot be empty.");
        }
        else break;
    }

    //Display food items
    List<FoodItem> foods = restaurant.MenuList.Count > 0 ? restaurant.MenuList[0].FoodItems : new List<FoodItem>();
    if (foods.Count == 0)
    {
        Console.WriteLine("No food items available for this restaurant. Order cannot be created.");
        return;
    }

    Console.WriteLine("\nAvailable Food Items:");
    for (int i = 0; i < foods.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {foods[i].ItemName} - ${foods[i].ItemPrice:F2}");
    }

    //Create Order and select items
    Order customerOrder = new Order { OrderedFoodItems = new List<OrderedFoodItem>() };
    while (true)
    {
        Console.Write("Enter item number (0 to finish): ");
        string input = Console.ReadLine()?.Trim();
        if (!int.TryParse(input, out int itemNum) || itemNum < 0 || itemNum > foods.Count)
        {
            Console.WriteLine("Invalid item number. Please try again.");
            continue;
        }
        if (itemNum == 0) break;

        int quantity;
        while (true)
        {
            Console.Write("Enter quantity: ");
            string qtyInput = Console.ReadLine()?.Trim();
            if (!int.TryParse(qtyInput, out quantity) || quantity <= 0)
            {
                Console.WriteLine("Invalid quantity. Must be a positive integer.");
            }
            else break;
        }

        FoodItem selected = foods[itemNum - 1];
        OrderedFoodItem foodItem = new OrderedFoodItem
        {
            ItemName = selected.ItemName,
            ItemPrice = selected.ItemPrice,
            QtyOrdered = quantity
        };
        foodItem.SubTotal = foodItem.CalculateSubtotal();
        customerOrder.AddOrderedFoodItem(foodItem);
    }

    if (customerOrder.OrderedFoodItems.Count == 0)
    {
        Console.WriteLine("No items selected. Order cancelled.");
        return;
    }

    //Special request
    Console.Write("Add special request? [Y/N]: ");
    string specialReq = Console.ReadLine()?.Trim().ToUpper();
    if (specialReq == "Y")
    {
        Console.Write("Enter special request: ");
        string request = Console.ReadLine();
        customerOrder.OrderedFoodItems.ForEach(f => f.Customise = request);
    }

    //Calculate total
    double deliveryFee = 5.0;
    customerOrder.OrderTotal = customerOrder.CalculateOrderTotal() + deliveryFee;

    //BONUS FEATURE
    if (customer.ReferralPromo)
    {
        customerOrder.OrderTotal = customerOrder.OrderTotal * 0.8;
    }

    Console.WriteLine($"Order Total: ${customerOrder.OrderTotal-5 :F2} + $5.00 (delivery) = ${customerOrder.OrderTotal:F2}");

    //Payment method
    while (true)
    {
        Console.Write("Proceed to payment? [Y/N]: ");
        string pay = Console.ReadLine()?.Trim().ToUpper();
        if (pay == "Y")
        {
            Console.WriteLine("Payment method:");
            Console.Write("[CC] Credit Card / [PP] PayPal / [CD] Cash on Delivery: ");
            string method = Console.ReadLine()?.Trim().ToUpper();
            if (method != "CC" && method != "PP" && method != "CD")
            {
                Console.WriteLine("Invalid payment method. Try again.");
                continue;
            }
            customerOrder.OrderPaymentMethod = method;
            customerOrder.OrderPaid = true;
            break;
        }
        else if (pay == "N")
        {
            customerOrder.OrderPaymentMethod = "Not Paid";
            customerOrder.OrderPaid = false;
            break;
        }
        else
        {
            Console.WriteLine("Please enter Y or N.");
        }
    }

    //Generate Order ID & assign info
    int newOrderId = OrderList.Count > 0 ? OrderList.Max(o => o.OrderId) + 1 : 1001;
    customerOrder.OrderId = newOrderId;
    customerOrder.OrderStatus = "Pending";
    customerOrder.DeliveryDateTime = deliveryDateTime;
    customerOrder.DeliveryAddress = address;
    customerOrder.OrderDateTime = DateTime.Now;

    //Add to lists
    customer.AddOrder(customerOrder);
    restaurant.OrderList.Add(customerOrder);
    OrderList.Add(customerOrder);

    //Append to CSV
    using (StreamWriter sw = new StreamWriter("orders.csv", append: true))
    {
        string itemsStr = string.Join("|", customerOrder.OrderedFoodItems.Select(f => $"{f.ItemName},{f.QtyOrdered}"));
        sw.WriteLine($"{customerOrder.OrderId},{customer.EmailAddress},{restaurant.RestaurantId},{deliveryDateTime:dd/MM/yyyy},{deliveryDateTime:HH:mm},{address},{customerOrder.OrderDateTime:dd/MM/yyyy HH:mm},{customerOrder.OrderTotal},{customerOrder.OrderStatus},{itemsStr}");
    }

    Console.WriteLine($"Order {customerOrder.OrderId} created successfully! Status: {customerOrder.OrderStatus}");
}


//DISPLAY LOADING OF FILES
Console.WriteLine("Welcome to the Gruberoo Food Delivery System");
Console.WriteLine($"{RestaurantList.Count} restaurants loaded!");
int totalFoodItems = RestaurantList.Sum(r => r.MenuList.Count > 0 ? r.MenuList[0].FoodItems.Count : 0);
Console.WriteLine($"{totalFoodItems} food items loaded!");
Console.WriteLine($"{CustomerList.Count} customers loaded!");
Console.WriteLine($"{OrderList.Count} orders loaded!");
Console.WriteLine();


while (true)
{
    //DISPLAY MAIN MENU
    Console.WriteLine("===== Gruberoo Food Delivery System =====");
    Console.WriteLine("1. List all restaurants and menu items");
    Console.WriteLine("2. List all orders");
    Console.WriteLine("3. Create a new order");
    Console.WriteLine("4. Display Total Order Amount");
    Console.WriteLine("5. Add new customer");
    Console.WriteLine("0. Exit");
    Console.Write("Enter your choice: ");

    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            LoadMenuItems();  // displays restaurants & menu
            break;
        case "2":
            DisplayOrder();   // displays all orders
            break;
        case "3":
            CreateOrder();    // creates a new order
            break;
        case "4":
            DisplayTotalOrder();    // displays total order amount
            break;
        case "5":
            CreateNewCustomer();    // adds new customer and asks for referral code to be entitled to a discount
            break;
        case "0":
            Console.WriteLine("Exiting program. Goodbye!");
            return;
        default:
            Console.WriteLine("Invalid choice. Please try again.");
            break;
    }

    Console.WriteLine(); 
}

//ADVANCED FEATURE OPTION(B)
void DisplayTotalOrder()
{
    double totalOrderAmount = 0;
    double totalRefundedAmount = 0;
    double gruberooEarnings = 0;

    foreach (Restaurant r in RestaurantList)
    {
        foreach (Order o in r.OrderList)
        {
            // Skip empty orders
            if (o.OrderedFoodItems == null || o.OrderedFoodItems.Count == 0)
                continue;

            if (o.OrderStatus == "Delivered")
            {
                totalOrderAmount += (o.OrderTotal - 5);
                gruberooEarnings += 5;
            }
            else if (o.OrderStatus == "Cancelled")
            {
                foreach (OrderedFoodItem ofi in o.OrderedFoodItems)
                {
                    totalRefundedAmount += ofi.SubTotal;
                }
            }
        }
    }

    Console.WriteLine("___________________________");
    Console.WriteLine("Printing Total...");
    Console.WriteLine($"Total Order Amount: ${totalOrderAmount:F2}");
    Console.WriteLine($"Total Refunds: ${totalRefundedAmount:F2}");
    Console.WriteLine($"Final Amount Gruberoo Earns: ${gruberooEarnings}");
}

//ADD NEW CUSTOMER FOR BONUS FEATURE
void CreateNewCustomer()
{
    Console.Write("Enter you name: ");
    string name = Console.ReadLine();
    Console.Write("Enter your email: ");
    string email = Console.ReadLine();
    Console.Write("Enter referral code if any: ");
    string referralCode = Console.ReadLine();
    bool ReferralPromo = false;
    foreach (Customer c1 in CustomerList)
    {
        if (c1.ReferralCode == referralCode)
        {
            ReferralPromo = true;
            Console.WriteLine("You are entitled to a 20% discount on you first order in Gruberoo!");
            break;
        }
        else
        {
            ReferralPromo = false;
            Console.WriteLine("Your referral code is invalid.");
        }

    }
    Customer c = new Customer(email,name,ReferralPromo);

}


