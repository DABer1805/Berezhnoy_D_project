namespace Server;

using System;
using System.ComponentModel.DataAnnotations;

public class SalesReportItem
{
    public string ProductName { get; set; }
    public int TotalQuantity { get; set; }
    public decimal TotalAmount { get; set; }
}