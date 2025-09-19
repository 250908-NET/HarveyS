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
    public List<Tasc> listItems(string? filter, string? sort, DateTime? date, bool? completed, Prio? priority)
    {
        List<Tasc> sortList = new List<Tasc>();
        bool isEmpty = !theList.Any();
        if(isEmpty)
        {
            return null;
        }
        if((filter == "priority" || filter == "Priority" ) && theList.Count() >= 2) {
            foreach(Tasc i in theList) 
            {
                if(i.priority == priority) {
                    sortList.Add(i);
                }
            }
            if(!sortList.Any()) {
                return null;
            }
        } else if((filter == "completed" || filter == "Completed" ) && theList.Count() >= 2) {
            foreach(Tasc i in theList) 
            {
                if(i.isCompleted == completed) {
                    sortList.Add(i);
                }
            }
            if(!sortList.Any()) {
                return null;
            }
        } else if((filter == "duedate" || filter == "dueDate" ) && theList.Count() >= 2) {
            foreach(Tasc i in theList) 
            {
                if(i.dueDate < date) {
                    sortList.Add(i);
                }
            }
            if(!sortList.Any()) {
                return null;
            }
        } else {
            sortList = theList;
        }

        if((sort == "priority" || sort == "Priority" ) && theList.Count() >= 2) {
            sortList = sortList.OrderBy(theList => theList.priority).ToList();
        } else if((sort == "created" || sort == "Created" ) && theList.Count() >= 2) {
            sortList = sortList.OrderBy(theList => theList.isCompleted).ToList();
        } else if((sort == "dueDate" || sort == "dueDate" ) && theList.Count() >= 2) {
            sortList = sortList.OrderBy(theList => theList.dueDate).ToList();
        } else {
            return sortList;
        }
        return theList;
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

        task.updatedAt = DateTime.Now;

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

    public string stats() 
    {
        int crit = 0;
        int high = 0;
        int med = 0;
        int low = 0;
        int total = theList.Count;
        int completed = listItems("completed", null, null, true, null).Count;
        int overdue = listItems("dueBefore", null, DateTime.Now, null, null).Count;
        if(listItems("priority", null, null, null, Prio.Critical) != null)
            crit = listItems("priority", null, null, null, Prio.Critical).Count;
        if(listItems("priority", null, null, null, Prio.High) != null)
            high = listItems("priority", null, null, null, Prio.High).Count;
        if(listItems("priority", null, null, null, Prio.Medium) != null)
            med = listItems("priority", null, null, null, Prio.Medium).Count;
        if(listItems("priority", null, null, null, Prio.Low) != null)
            low = listItems("priority", null, null, null, Prio.Low).Count;

        return $"Total Tasks = {total}  |  Completed Tasks = {completed}  |  Overdue Tasks = {overdue}  |  Critical Tasks = {crit}  |  High Tasks = {high}  |  Medium Tasks = {med}  |  Low Tasks = {low}";
    }
}