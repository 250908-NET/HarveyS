
public class ToDoService
{
    private List<ToDoItem> theList = new List<ToDoItem>();

    public string addToList(ToDoItem newItem)
    {
        theList.Add(newItem);
    }

    public string listItems()
    {
        return toDo;
    }

    public string markItem(ToDoItem item)
    {
        if(item.isCompleted == false) 
        {
            item.isCompleted = true;
        } else {
            item.isCompleted = false;
        }
        
    }

    public string deleteItem(ToDoItem item)
    {
        
    }


}