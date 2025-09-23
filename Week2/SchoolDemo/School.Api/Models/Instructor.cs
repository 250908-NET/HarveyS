using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace School.Models;

public class Instructor
{
    //Lets use defaults here
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string FirstName { get; set; }
    
    [Required, MaxLength(50)]
    public string LastName { get; set; }
    List<Course> Courses { get; set; } = new();
}