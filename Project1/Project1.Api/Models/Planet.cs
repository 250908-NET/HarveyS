using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Space.Models;

public class Planet
{
    [Key]
    public int PlanetId { get; set; }
    
    [Required, MaxLength(50)]
    public string Name { get; set; }
    
    [Required, MaxLength(50)]
    public string Description { get; set; }
    [JsonIgnore]
    public List<Star> stars { get; set; } = new();
    [JsonIgnore]
    public List<Moon> moons { get; set; } = new();
}