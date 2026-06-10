namespace RestCars.Models
{
    public class CarsRepoDatabase: ICarsRepo
    {
        private readonly CarsDbContext _context;
        public CarsRepoDatabase(CarsDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Car> GetAllCars(int? minimumYear, int? maximumYear)
        {
            IQueryable<Car> query = _context.Cars;
            if (minimumYear != null)
            {
                query = query.Where(c => c.Year >= minimumYear);
            }
            if (maximumYear != null)
            {
                query = query.Where(c => c.Year <= maximumYear);
            }
            return query.ToList();
        }
        public IEnumerable<Car> GetAllCars()
        {
            throw new NotImplementedException();
        }

        public Car? GetCarById(int id)
        {
            return _context.Cars.Find(id);
        }
        public Car AddCar(Car car)
        {
            if (car == null)
            {
                throw new ArgumentNullException(nameof(car));
            }
            _context.Cars.Add(car);
            _context.SaveChanges();
            return car;
        }

        public Car? RemoveCar(int id)
        {
            var car = GetCarById(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                _context.SaveChanges();
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
                _context.SaveChanges();
                return existingCar;
            }
            return null;
        }
    }
}
