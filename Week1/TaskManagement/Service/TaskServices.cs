namespace taskManagement.service;
using taskManagement.models;

public class TaskService
{
    private List<Tasc> theList = new List<Tasc>();

    public Tasc addToList(Tasc task)
    {
        Tasc newTask = new Tasc(task.title, task.description, task.isCompleted, task.priority, task.dueDate);
        theList.Add(newTask);
        return newTask;
    }

    public List<Tasc> listItems(string sort)
    {
        bool isEmpty = !theList.Any();
        if(isEmpty)
        {
            return null;
        }
        if((sort == "priority" || sort == "Priority" ) && theList.Count() >= 2) {
            theList.OrderBy(theList => theList.priority);
        }
        if((sort == "completed" || sort == "Completed" ) && theList.Count() >= 2) {
            theList.OrderBy(theList => theList.isCompleted);
        }
        if((sort == "duedate" || sort == "dueDate" ) && theList.Count() >= 2) {
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

        theList[id] = task;

        return task;
    }

    public string deleteTask(int id)
    {
        string deletedTask = theList[id].title;
        theList.Remove(theList[id-1]);
        return deletedTask;
    }
}