using CarsNMore.model;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Cars N more starting");  
        Car myCar = new Car();
        Console.WriteLine("new car created");

        myCar.make = "Toyota";
        myCar.model = "Corola";

        Console.WriteLine(myCar.describeCar());
    }
}