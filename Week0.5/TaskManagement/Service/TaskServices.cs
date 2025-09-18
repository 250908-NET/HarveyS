namespace taskManagement.service;
using System.Text.RegularExpressions;
using taskManagement.models;

public class TaskService
{
    private List<Tasc> theList = new List<Tasc>();

    public bool isValidDate(string date)
    {
        string reg = @"^\d{4}\-(0?[1-9]|1[012])\-(0?[1-9]|[12][0-9]|3[01])$";
        bool isValid = Regex.IsMatch(date, reg);
        if(isValid || date == null || date == "") {
            return true;
        }
        return false;
    }

    public Tasc addToList(Tasc task)
    {
        Tasc newTask = new Tasc(task.title, task.description, task.isCompleted, task.priority, task.dueDate);
        theList.Add(newTask);
        return newTask;
    }

    //List all tasks, and if specified, sort them
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

    //find any task by ID
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

    //Update task values
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
            task.dueDate = dueDate;
        }
        task.UpdatedAt = DateTime.Now;

        theList[id-1] = task;

        return task;
    }

    //Remove a task from the list
    public string deleteTask(int id)
    {
        string deletedTask = theList[id-1].title;
        theList.Remove(theList[id-1]);
        return deletedTask;
    }
}