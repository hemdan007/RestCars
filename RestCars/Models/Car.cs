namespace RestCars.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        public override string ToString()
        {
            return $"Car(Id={Id}, Brand={Brand}, Model={Model}, Year={Year})";
        }
    }
}
