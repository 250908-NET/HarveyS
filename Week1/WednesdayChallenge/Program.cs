using wednesday.model;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Challenge starting");  
        ToDoItem myCar = new ToDoItem();
        Console.WriteLine("new item created");

        Console.WriteLine(myCar.describeCar());
    }
}