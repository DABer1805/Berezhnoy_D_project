namespace Server;

using System;
using System.ComponentModel.DataAnnotations;

// Определение класса Puzzle (Головоломка)
public class Puzzle
{
    // Свойства класса Puzzle

    // Свойство Title (Название)
    [Required]
    [StringLength(2000, MinimumLength = 3)]
    public string Title { get; set; }

    // Свойство SheetType (Тип листа) - тип используемого фанерного листа
    [Required]
    [StringLength(2000, MinimumLength = 3)]
    public string SheetType { get; set; }

    // Свойство PieceCount (Количество деталей)
    [Required]
    [Range(1, 2000)]
    public int PieceCount { get; set; }

    // Свойство Price (Цена)
    [Required]
    [Range(0, 100000)]
    public decimal Price { get; set; }

    // Конструктор класса Puzzle
    public Puzzle(string title, string sheetType, int pieceCount, decimal price)
    {
        Title = title; // Инициализация свойства Title переданным значением названия головоломки.
        SheetType = sheetType; // Инициализация свойства SheetType переданным значением типа листа.
        PieceCount = pieceCount; // Инициализация свойства PieceCount переданным значением количества деталей.
        Price = price; // Инициализация свойства Price переданным значением цены.
    }
}
