namespace wednesday.model;
public class ToDoItem
{
    public int ID { get; set; }
    public string Title { get; set; }
    public bool isCompleted { get; set; }
    public DateTime createdDate { get; set; }
    
    public List()
    {
        ID = 0;
        Title = "Air";
        isCompleted = false;
        createdDate = DateTime.Today;

    }   

    public List(int numberOfItems, List list)
    {
        this.numberOfItems = numberOfItems;
        New List toDo = list;
    }

    /*
    === TO-DO LIST MANAGER ===
1. Add new item
2. View all items
3. Mark item complete
4. Mark item incomplete
5. Delete item
6. Exit

Choose an option (1-6): 1

Enter task description: Buy groceries
Task added successfully! (ID: 1)

Choose an option (1-6): 2

=== YOUR TO-DO ITEMS ===
[1] Buy groceries (Created: 1/15/2024) - ⭕ Not Complete
*/

}