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

    public List<Tasc> listItems()
    {
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

    public string markTask(int id)
    {
        Tasc task = findTask(id);
        if(task.isCompleted == false) 
        {
            task.isCompleted = true;
        } else {
            task.isCompleted = false;
        }
        return "Success";
    }

    public string deleteTask(int id)
    {
        string deletedTask = findTask(id).title;
        theList.Remove(findTask(id));
        return deletedTask;
    }


}