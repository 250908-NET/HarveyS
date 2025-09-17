//namespace declaration - a way to organize code
namespace CarsNMore.model;
//Every class has members   
//Members are either fields or methods


public class Vehicle
{
    //Fields - values and variables that the object/class contains
    public double mileage { get; set; }
    public int passengerCapacity { get; set; }
    public string make { get; set; }
    public string model { get; set; }
    public double resaleValue { get; set; } //decimal is possible
    public string VIN { get; set; }

    //Methods - functions or behaviors that the object/class can perform
    //Constructor - a special object that is called when an object is created that defines how to create an object

    public Vehicle() {
        mileage = 0;
        passengerCapacity = 1;
        make = "";
        model = "";
        resaleValue = 0;
        VIN = "0000000000000000";
    }

    public Vehicle(double mileage, int passengerCapacity, string make, string model, double resaleValue, string VIN)
    {
        this.mileage = mileage;
        this.passengerCapacity = passengerCapacity;
        this.make = make;
        this.model = model;
        this.resaleValue = resaleValue;
        this.VIN = VIN;
    }

}