namespace Server;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public enum OrderStatus
{
    Draft,
    AgreedWithClient,
    InProduction,
    ReadyForShipment,
    ShippedToClient
}

public class OrderItem
{
    [Required]
    [StringLength(2000, MinimumLength = 3)]
    public string Name { get; set; }
    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
    [Required]
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }
}

public class Order
{
    public int Id { get; set; }
    [Required]
    public OrderStatus Status { get; set; }
    [Required]
    public DateTime RegistrationDate { get; set; }
    [Required]
    [StringLength(2000, MinimumLength = 3)]
    public string Client { get; set; }
    public DateTime? CompletionDate { get; set; }
    [Required]
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();

    public Order()
    {
        RegistrationDate = DateTime.Now;
    }
}