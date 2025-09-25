using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Space.Models;

public class Star
{
    //Lets use defaults here
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Name { get; set; }
    
    [Required, MaxLength(50)]
    public string Description { get; set; }
    //SolarSystem solarSystem { get; set; }
    List<Planet> Planets { get; set; } = new();
}