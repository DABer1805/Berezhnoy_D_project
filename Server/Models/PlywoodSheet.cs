namespace Server;

using System;
using System.ComponentModel.DataAnnotations;

// Определение класса PlywoodSheet (Фанерный лист)
public class PlywoodSheet
{
    // Свойства класса PlywoodSheet

    // Свойство Title (Название)
    [Required]
    [StringLength(2000, MinimumLength = 3)]
    public string Title { get; set; }

    // Свойство Material (Материал)
    [Required]
    [StringLength(2000, MinimumLength = 3)]
    public string Material { get; set; }

    // Свойство Thickness (Толщина)
    [Required]
    [Range(2, 20)]
    public int Thickness { get; set; }
    // Конструктор класса PlywoodSheet
    public PlywoodSheet(string title, string material, int thickness)
    {
        Title = title; // Инициализация свойства Title переданным значением.
        Material = material; // Инициализация свойства Material переданным значением.
        Thickness = thickness; // Инициализация свойства Thickness переданным значением.
    }
}
