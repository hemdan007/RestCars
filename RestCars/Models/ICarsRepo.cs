namespace RestCars.Models
{
    public interface ICarsRepo
    {

        Car AddCar(Car car);
        IEnumerable<Car> GetAllCars(int? minimumYear, int? maximumYear);
        IEnumerable<Car> GetAllCars();
        Car? GetCarById(int id);
        Car? RemoveCar(int id);
        Car? UpdateCar(int id, Car updatedCar);
    }
}
