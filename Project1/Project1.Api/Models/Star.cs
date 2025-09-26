using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Space.Models;

public class Star
{
    //Lets use defaults here
    [Key]
    public int StarId { get; set; }

    [Required, MaxLength(50)]
    public string Name { get; set; }
    
    [Required, MaxLength(50)]
    public string Description { get; set; }
    [JsonIgnore]
    public List<Planet> Planets { get; set; } = new();
}