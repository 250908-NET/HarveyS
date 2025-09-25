using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Space.Models;

public class Moon
{
    [Key]
    public int MoonId { get; set; }
    
    [Required, MaxLength(50)]
    public string Name { get; set; }
    
    [Required, MaxLength(50)]
    public string Description { get; set; }
    public Planet planet { get; set; }
}