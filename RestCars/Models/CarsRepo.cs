namespace RestCars.Models
{
    public class CarsRepo : ICarsRepo
    {
        private readonly List<Car> cars = new List<Car>();
        private int nextId = 1;

        public CarsRepo(bool includeData = false)
        {
            if (includeData)
            {
                AddCar(new Car { Brand = "Toyota", Model = "Corolla", Year = 2020 });
                AddCar(new Car { Brand = "Honda", Model = "Civic", Year = 2019 });
                AddCar(new Car { Brand = "Ford", Model = "Focus", Year = 2018 });
                AddCar(new Car { Brand = "Skoda", Model = "Enyaq", Year = 2021 });
                AddCar(new Car { Brand = "Chevrolet", Model = "Optra", Year = 2017 });
            }
        }

        public IEnumerable<Car> GetAllCars(int? minimumYear = null, int? maximumYear = null)
        {
            IEnumerable<Car> result = cars.AsReadOnly();
            if (minimumYear != null)
            {
                result = result.Where(c => c.Year >= minimumYear);
            }
            if (maximumYear != null)
            {
                result = result.Where(c => c.Year <= maximumYear);
            }
            return result;
        }
        public IEnumerable<Car> GetAllCars()
        {
            throw new NotImplementedException();
        }
        public Car? GetCarById(int id)
        {
            return cars.FirstOrDefault(c => c.Id == id);
        }
        public Car AddCar(Car car)
        {
            if (car == null)
            {
                throw new ArgumentNullException(nameof(car));
            }
            car.Id = nextId++;
            cars.Add(car);
            return car;
        }

        public Car? RemoveCar(int id)
        {
            var car = GetCarById(id);
            if (car != null)
            {
                cars.Remove(car);
                return car;
            }
            return null;
        }
        public Car? UpdateCar(int id, Car updatedCar)
        {
            var existingCar = GetCarById(id);
            if (existingCar != null)
            {
                existingCar.Brand = updatedCar.Brand;
                existingCar.Model = updatedCar.Model;
                existingCar.Year = updatedCar.Year;
                return existingCar;
            }
            return null;
        }
    }
}
