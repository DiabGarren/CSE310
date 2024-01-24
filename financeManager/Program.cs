using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;


var currentDir = Directory.GetCurrentDirectory();
var productsDir = Path.Combine(currentDir, "assests", "products.json");
var ordersDir = Path.Combine(currentDir, "assests", "orders.json");

while (true)
{
    Console.WriteLine($"{Environment.NewLine}Would you like to create a purchase order, view all purchase orders, create a new product, view all products or exit?{Environment.NewLine}1. Create a purchase order{Environment.NewLine}2. View all orders{Environment.NewLine}3. Create a new product{Environment.NewLine}4. View all products{Environment.NewLine}5. Exit");
    var method = Console.ReadLine();
    switch (method)
    {
        case "1":
            break;
        case "2":
            Console.WriteLine($"{Environment.NewLine}Generating purchase orders...{Environment.NewLine}");
            foreach (var order in GetOrders())
            {
                displayOrder(order);
            }
            break;
        case "3":
            Console.Write($"{Environment.NewLine}Enter the item name: ");
            var name = Console.ReadLine();
            Console.Write("Enter the item colour: ");
            var colour = Console.ReadLine();
            Console.Write("Enter the item price: ");
            var price = Convert.ToDouble(Console.ReadLine());
            Console.Write("Enter the item quantity: ");
            var qty = Convert.ToInt32(Console.ReadLine());
            createItem(name!, colour!, price!, qty!);
            break;
        case "4":
            Console.WriteLine($"{Environment.NewLine}Generating product list...{Environment.NewLine}");
            displayProducts(GetProducts());

            break;
        default:
            return;
    }
}


IEnumerable<ProductData> GetProducts()
{
    string itemsJson = File.ReadAllText(productsDir);
    IEnumerable<ProductData>? data = JsonConvert.DeserializeObject<IEnumerable<ProductData>>(itemsJson);

    return data!;
}

IEnumerable<OrderData> GetOrders()
{
    string itemsJson = File.ReadAllText(ordersDir);
    IEnumerable<OrderData>? data = JsonConvert.DeserializeObject<IEnumerable<OrderData>>(itemsJson);

    return data!;
}

void createItem(string name, string colour, double price, int qty)
{
    List<ProductData> products = [];
    foreach (var product in GetProducts())
    {
        products.Add(product);
    }
    products.Add(new ProductData(name, colour, price, qty));

    var json = JsonConvert.SerializeObject(products, Formatting.Indented);

    File.WriteAllText(productsDir, json);
}

void displayOrder(OrderData order)
{
    Console.WriteLine($"Order: order-{order.OrderNumber}{Environment.NewLine}Customer Name: {order.Name}{Environment.NewLine}Order Date: {order.Date}{Environment.NewLine}{Environment.NewLine}");
    displayProducts(order.Products);
    Console.WriteLine($"{Environment.NewLine}\t\tTotal: R{order.Total}{Environment.NewLine}---------------------------------{Environment.NewLine}");
}
void displayProducts(IEnumerable<ProductData> products)
{
    Console.WriteLine("Name\tColour\tPrice\tQuantity");
    foreach (var product in products)
    {
        Console.WriteLine($"{product.Name}\t{product.Colour}\tR{product.Price}\t{product.Qty}");
    }
}

record ProductData(string Name, string Colour, double Price, int Qty);
record OrderData(int OrderNumber, string Name, string Date, IEnumerable<ProductData> Products, double Total);