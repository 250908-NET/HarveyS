namespace taskManagement.models;

public class Tasc
{
    private static int currentId = 0;
    public int ID { get; set; }
    public string title { get; set; }
    public string? description { get; set; }
    public bool isCompleted { get; set; }
    // public enum priority 
    // {
    //     Low,
    //     Medium,
    //     High,
    //     Critical
    // }
    public DateTime? dueDate { get; set; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; set; }

    public Tasc()
    {
        ID = Interlocked.Increment(ref currentId);
        title = "Default Task";
        description = "Does stuff";
        isCompleted = false;
        //priority priority = priority.Low;
        dueDate = DateTime.Today.AddDays(7);
        CreatedAt = DateTime.Now;

    }

    public Tasc(string title, string description, bool isCompleted, DateTime dueDate) //priority priority, DateTime? dueDate
    {
        this.ID = Interlocked.Increment(ref currentId);
        this.title = title;
        if(description != null) {
            this.description = description;
        }
        this.isCompleted = isCompleted;
        //this.priority = priority;
        if(dueDate != null) {
            this.dueDate = dueDate;
        }
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
