using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

var currentDir = Directory.GetCurrentDirectory();
var itemsDir = Path.Combine(currentDir, "assests", "items.json");
var ordersDir = Path.Combine(currentDir, "assests", "orders.json");

var items = GetItems(itemsDir);
var orders = GetOrders(ordersDir);

foreach (var order in orders)
{
    displayOrder(order);
}

IEnumerable<ItemData> GetItems(string fileName)
{
    string itemsJson = File.ReadAllText(fileName);
    IEnumerable<ItemData>? data = JsonConvert.DeserializeObject<IEnumerable<ItemData>>(itemsJson);

    return data;
}

IEnumerable<OrderData> GetOrders(string fileName)
{
    string itemsJson = File.ReadAllText(fileName);
    IEnumerable<OrderData>? data = JsonConvert.DeserializeObject<IEnumerable<OrderData>>(itemsJson);

    return data;
}

void displayOrder(OrderData order)
{
    Console.WriteLine($"Order: order-{order?.orderNumber}");
    Console.WriteLine($"Customer Name: {order?.name}");
    Console.WriteLine($"Order Date: {order?.date}{Environment.NewLine}");
    Console.WriteLine($"Name\tColour\tPrice\tQuantity");
    foreach (var item in order?.items)
    {
        Console.WriteLine($"{item?.name}\t{item?.colour}\tR{item?.price}\t{item?.qty}");
    }
    Console.WriteLine($"{Environment.NewLine}\t\tTotal: R{order?.total}{Environment.NewLine}---------------------------------{Environment.NewLine}");
}

record ItemData(string name, string colour, double price, int qty);

record OrderData(int orderNumber, string name, string date, IEnumerable<ItemData> items, double total);