using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;//to customize column name

namespace School.Models;

public class Student
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Key already does this
    public int Id { get; set; } 

    [Required]
    [MaxLength(50)]
    //[Column("FirstName")] //Sets the column name
    public string FirstName { get; set; }

    [Required, MaxLength(50)]
    public string LastName { get; set; }

    [Required]
    public string Email { get; set; }

    List<Course> Courses { get; set; } = new();
}