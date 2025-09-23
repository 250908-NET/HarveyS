using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Space.Models;

public class Moon
{
    public int Id { get; set; }
    
    [Required, MaxLength(50)]
    public string Name { get; set; }
    
    [Required, MaxLength(50)]
    public string Description { get; set; }
    //public SolarSystem SolarSystem { get; set; }
    public Star star { get; set; }
    List<Moon> Moons { get; set; } = new();
}