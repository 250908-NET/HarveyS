namespace taskManagement.service;
using taskManagement.models;

public class TaskService
{
    private List<Tasc> theList = new List<Tasc>();

    public string addToList(Tasc newItem)
    {
        theList.Add(newItem);
        return newItem.title;
    }

    public List<Tasc> listItems(string sort)
    {
        bool isEmpty = !theList.Any();
        if(isEmpty)
        {
            return null;
        }
        if((sort == "priority" || sort == "Priority" ) && theList.Count() >=2) {
            theList.OrderBy(theList => theList.priority);
        }
        if((sort == "completed" || sort == "Completed" ) && theList.Count() >=2) {
            theList.OrderBy(theList => theList.isCompleted);
        }
        if((sort == "duedate" || sort == "dueDate" ) && theList.Count() >=2) {
            theList.OrderBy(theList => theList.dueDate);
        }
        return theList;
    }

    public Tasc findTask(int id)
    {
        foreach(Tasc i in theList) 
        {
            if(i.ID == id) {
                return i;
            }
        }
        return null;
    }

    public Tasc updateTask(int id)
    {
        Tasc task = findTask(id);

        // task.title = title;
        // task.description = description;
        // task.isCompleted = isCompleted;
        // task.priority = priority;
        // task.dueDate = dueDate;
        // task.CreatedAt = DateTime.Now;

        return task;
    }

    public string deleteTask(int id)
    {
        string deletedTask = findTask(id).title;
        theList.Remove(findTask(id));
        return deletedTask;
    }
}