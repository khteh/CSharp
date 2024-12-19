using System;

namespace CSharp;
public class PricingDetails
{
    public decimal Total { get; set; }
    public PricingDetails(decimal total) => Total = total;
}
public class Order
{
    public PricingDetails Price { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public Order(DateTimeOffset dt, PricingDetails price) => (Timestamp, Price) = (dt, price);
}
public class MonthSales
{
    public int Month { get; set; }
    public decimal Sales { get; set; }
}