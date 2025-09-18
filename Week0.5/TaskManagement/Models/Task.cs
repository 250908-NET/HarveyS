namespace taskManagement.models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public enum Prio 
{
    Low,
    Medium,
    High,
    Critical
}

public class Tasc
{
    private static int currentId = 0;
    public int ID { get; }
    [Required]
    [MaxLength(100)]
    public string title { get; set; }
    [MaxLength(500)]
    public string? description { get; set; }
    public bool isCompleted { get; set; }
    public Prio priority { get; set; }
    public string? dueDate { get; set; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; set; }

    public Tasc()
    {
        this.ID = currentId;
        title = "Default Task";
        isCompleted = false;
        priority = 0;
        CreatedAt = DateTime.Now;

    }

    public Tasc(string title, string description, bool isCompleted, Prio priority, string? dueDate) //priority priority, DateTime? dueDate
    {
        this.ID = Interlocked.Increment(ref currentId);
        this.title = title;
        this.description = description;
        this.isCompleted = isCompleted;
        this.priority = priority;
        this.dueDate = dueDate;
        this.CreatedAt = DateTime.Now;
    }
    
    /*
    Id (int, primary key, auto-generated)
    Title (string, required, max 100 characters)
    Description (string, optional, max 500 characters)
    IsCompleted (bool, default false)
    Priority (enum: Low, Medium, High, Critical)
    DueDate (DateTime, optional)
    CreatedAt (DateTime, auto-generated)
    UpdatedAt (DateTime, auto-updated)
    */
}
