using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace School.Models;

public class Course
{
    public int Id { get; set; }
    
    [Required, MaxLength(50)]
    public string Title { get; set; }
    
    [Required, MaxLength(50)]
    public string Description { get; set; }
    public Instructor Instructor { get; set; }
    List<Student> Students { get; set; } = new();
}