namespace taskManagement.service;
using taskManagement.models;

public class TaskService
{
    private List<Tasc> theList = new List<Tasc>();

    //Create a new task object, using this constructor generates a unique ID
    public Tasc addToList(Tasc task)
    {
        Tasc newTask = new Tasc(task.title, task.description, task.isCompleted, task.priority, task.dueDate);
        theList.Add(newTask);
        return newTask;
    }

    //List items, if a keyword representing a sorting order is specified, order the list in the way
    public List<Tasc> listItems(string sort)
    {
        List<Tasc> sortList = new List<Tasc>();
        bool isEmpty = !theList.Any();
        if(isEmpty)
        {
            return null;
        }
        if((sort == "priority" || sort == "Priority" ) && theList.Count() >= 2) {
            sortList = theList.OrderBy(theList => theList.priority).ToList();
        } else if((sort == "completed" || sort == "Completed" ) && theList.Count() >= 2) {
            sortList = theList.OrderBy(theList => theList.isCompleted).ToList();
        } else if((sort == "duedate" || sort == "dueDate" ) && theList.Count() >= 2) {
            sortList = theList.OrderBy(theList => theList.dueDate).ToList();
        } else {
            return theList;
        }
        return sortList;
    }

    //Iterate through list, if object is not found, return null, checks entire list without out of bounds exception
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

    //Find task object, then update it
    public Tasc? updateTasc(int id, string? title = null, string? description = null, bool? isCompleted = null, Prio? priority = null, string? dueDate = null)
    {
        Tasc task = findTask(id);

        task.UpdatedAt = DateTime.Now;

        if (title != null && title != "") 
        {
            task.title = title; 
        }
        if (isCompleted.HasValue)
        {
            task.isCompleted = isCompleted.Value;
        }
        if (priority.HasValue)
        {
            task.priority = priority.Value;
        }
        if (description != null)
        {
            task.description = description;
        }
        if(dueDate != null && dueDate != "")
        {
            task.dueDate = DateTime.Parse(dueDate);
        }

        theList[id-1] = task;

        return task;
    }

    //Delete a task from the list, does not update any ID
    public string deleteTask(int id)
    {
        string deletedTask = theList[id-1].title;
        theList.Remove(theList[id-1]);
        return deletedTask;
    }
}