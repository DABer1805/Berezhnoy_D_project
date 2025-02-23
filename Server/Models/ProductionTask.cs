namespace Server;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public enum ProductionTaskStatus
{
    New,
    InProgress,
    Completed,
    Cancelled
}

public class ProductionTaskItem
{
    public int Id { get; set; }
    [Required]
    [StringLength(2000, MinimumLength = 3)]
    public string Name { get; set; }
    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}

public class ProductionTask
{
    public int Id { get; set; }
    [Required]
    public ProductionTaskStatus Status { get; set; }
    [Required]
    public DateTime RegistrationDate { get; set; }
    public DateTime? CompletionDate { get; set; }
    [Required]
    public int OrderId { get; set; }
    [Required]
    public List<ProductionTaskItem> Items { get; set; } = new List<ProductionTaskItem>();
}
