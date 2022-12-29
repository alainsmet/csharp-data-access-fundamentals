using Microsoft.EntityFrameworkCore;
using WarehouseManagementSystem;

Customer filip = new()
{
    Id = Guid.NewGuid(),
    Name = "Alain Smet",
    Address = "Main street",
    PostalCode = "12345",
    Country = "France",
    PhoneNumber = "+40 0908070605"
};

ShippingProvider shippingProvider = new ShippingProvider() 
{
    Id = Guid.NewGuid(),
    Name = "Great Postal Service of Anvers",
    FreightCost = 100m
};

Item item = new Item()
{
    Id = Guid.NewGuid(),
    Name = "Sponge with square pants",
    Description = "As the name implies, a sponge with square pants",
    InStock = 10,
    Price = 120
};

Order order = new Order()
{
    Id = Guid.NewGuid(),
    Customer = filip,
    ShippingProvider = shippingProvider,
    LineItems = new LineItem[]
    {
        new LineItem()
        {
            Id = Guid.NewGuid(),
            Item = item,
            Quantity = 1
        }
    }
};

using var context = new WarehouseContext();
context.Database.Migrate();

context.Orders.Add(order);
context.SaveChanges();




// ============= Third example - Adding orders and items to orders =============
//
//using var context = new WarehouseContext();

//var firstCustomer = context.Customers.First();
//firstCustomer.Orders.Add(new()
//{
//    Id = Guid.NewGuid(),
//    LineItems = new LineItem[]
//    {
//        new()
//        {
//            Id = Guid.NewGuid(),
//            Item = context.Items.First(),
//            Quantity = 1,
//        }
//    },
//    ShippingProvider = context.ShippingProviders.First(),
//});

//context.Customers.Update(firstCustomer);
//context.SaveChanges();
//Console.WriteLine("Customer updated !");

// ============= Second example - Adding, updating and deleting data =============
//
//var toDelete = context.Customers
//    .First(customer => customer.Orders.Any());

//context.Customers.Remove(toDelete);

//context.SaveChanges();

//Console.Write("Enter customer name : ");
//var newCustomer = new Customer
//{
//    Name = Console.ReadLine(),
//    Address = "Kungsbacka",
//    PostalCode = "434 94",
//    Country = "Sweden",
//    PhoneNumber = "+46 111 111 11"
//};

//context.Customers.Add(newCustomer);
//context.SaveChanges();

//var toUpdate = context.Customers
//    .First(customer => customer.Name == "Betty Boop");

//toUpdate.Name = "Markus Herdberger";
//context.Customers.Update(toUpdate);
//context.SaveChanges();

//Console.ReadLine();

// ============= First example - Accessing data =============
//
//var customer = context.Customers
//    .FirstOrDefault(customer => customer.Name == "Filip Ekberg");

//foreach (var order in
//    context.Orders
//    .Where(order => order.CustomerId == customer.Id)
//    .Include(order => order.Customer)
//    .Include(order => order.ShippingProvider)
//    .Include(order => order.LineItems)
//    .ThenInclude(lineItem => lineItem.Item))
//{
//    Console.WriteLine($"Order Id : {order.Id}");
//    Console.WriteLine($"Customer : {order.Customer.Name}");
//    Console.WriteLine($"Shipping provider : {order.ShippingProvider.Name}");
//    foreach (var lineItem in order.LineItems)
//    {
//        Console.WriteLine($"\t{lineItem.Item.Name}");
//        Console.WriteLine($"\t{lineItem.Item.Price}");
//    }
//}

//Console.ReadLine();