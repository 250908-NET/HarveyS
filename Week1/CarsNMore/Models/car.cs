namespace CarsNMore.model;
public class Car : WheeledVehicle
{
    public int numberOfDoors { get; set; }
    public bool isElectric { get; set; }
    public bool hasMirrors { get; set; }

    public Car() : base()
    {
        numberOfDoors = 4;
        isElectric = false;
        hasMirrors = true;
    }   

    public Car(int numberOfDoors, bool isElectric, bool hasMirrors, int numberOfWheels, int wheelSize, double treadDepth, double mileage, int passengerCapacity, string make, string model, double resaleValue, string VIN) : base(numberOfWheels, wheelSize, treadDepth, mileage, passengerCapacity, make, model, resaleValue, VIN)
    {
        this.numberOfDoors = numberOfDoors;
        this.isElectric = isElectric;
        this.hasMirrors = hasMirrors;
    }

    public string describeCar()
    {
        return $"My car is a {this.make} {this.model}"; //string interpolation
        string concat = "My car is a " + this.make + " " + this.model;
    }

}