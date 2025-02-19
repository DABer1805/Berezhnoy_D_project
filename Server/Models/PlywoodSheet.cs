namespace Server;

using System;
using System.ComponentModel.DataAnnotations;

public class PlywoodSheet
{
    [Required]
    [StringLength(2000, MinimumLength = 3)]
    public string Title { get; set; }

    [Required]
    [StringLength(2000, MinimumLength = 3)]
    public string Material { get; set; }

    [Required]
    [Range(2, 20)]
    public int Thickness { get; set; }
    public PlywoodSheet(string title, string material, int thickness)
    {
        Title = title;
        Material = material;
        Thickness = thickness;
    }
}