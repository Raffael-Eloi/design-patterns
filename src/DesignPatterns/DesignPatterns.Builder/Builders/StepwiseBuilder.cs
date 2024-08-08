namespace DesignPatterns.Builder.Builders;

public enum CarType
{
    Sedan,
    Crossover
};

/*
 * The Sedan type of car should have wheel size between 15 and 17.
 * The Crossover type of car should have wheel size between 17 and 20.
*/

public class Car
{
    public CarType Type;
    public int WheelSize;

    public override string ToString()
    {
        return $"Car type: {Type}, wheel size: {WheelSize}";
    }
}

/*
 * The ISpecifyCarType interface is used to specify the type of car.
 * This interface has a method OfType that takes a CarType and returns an ISpecifyWheelSize.
*/

public interface ISpecifyCarType
{
    public ISpecifyWheelSize OfType(CarType type);
}

public interface ISpecifyWheelSize
{
    public IBuildCar WithWheels(int size);
}

public interface IBuildCar
{
    public Car Build();
}

public class CarBuilder
{
    public static ISpecifyCarType Create()
    {
        return new Impl();
    }

    /*
     * The Impl class is private to not expose the implementation details.
    */

    private class Impl :
      ISpecifyCarType,
      ISpecifyWheelSize,
      IBuildCar
    {
        private Car car = new Car();

        public ISpecifyWheelSize OfType(CarType type)
        {
            car.Type = type;
            return this;
        }

        public IBuildCar WithWheels(int size)
        {
            switch (car.Type)
            {
                case CarType.Crossover when size < 17 || size > 20:
                case CarType.Sedan when size < 15 || size > 17:
                    throw new ArgumentException($"Wrong size of wheel for {car.Type}.");
            }
            car.WheelSize = size;
            return this;
        }

        public Car Build()
        {
            return car;
        }
    }
}

class ProgramStepWiseBuilder
{
    static void MainStepwiseBuilder(string[] args)
    {
        var car = CarBuilder.Create()
          .OfType(CarType.Crossover)
          .WithWheels(18)
          .Build();
        Console.WriteLine(car);
    }
}