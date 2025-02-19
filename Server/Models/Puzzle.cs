namespace Server;

using System;
using System.ComponentModel.DataAnnotations;

public class Puzzle
{
    [Required]
    [StringLength(2000, MinimumLength = 3)]
    public string Title { get; set; }
    [Required]
    [StringLength(2000, MinimumLength = 3)]
    public string SheetType { get; set; }
    [Required]
    [Range(1, 2000)]
    public int PieceCount { get; set; }
    [Required]
    [Range(0, 100000)]
    public decimal Price { get; set; }
    public Puzzle(string title, string sheetType, int pieceCount, decimal price)
    {
        Title = title;
        SheetType = sheetType;
        PieceCount = pieceCount;
        Price = price;
    }
}