using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestCars.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestCars.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        // Dependency injection of the repository
        private readonly ICarsRepo _repo;
        // Constructor to receive the repository instance
        public CarsController(ICarsRepo repo)
        {
            _repo = repo;
        }

        // GET: api/<CarsController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public ActionResult<IEnumerable<Car>> Get([FromQuery] int? minimumYear, [FromQuery] int? maximumYear)
        {
            if (minimumYear > maximumYear)
            {
                return NotFound();
            }
            IEnumerable<Car> result = _repo.GetAllCars(minimumYear, maximumYear);
            return Ok(result);
        }

        // GET api/<CarsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<Car> Get(int id)
        {
            Car? car = _repo.GetCarById(id);
            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }

        // POST api/<CarsController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<Car> Post([FromBody] Car newCar)
        {
            try
            {
                _repo.AddCar(newCar);
                return Created($"api/cars/{newCar.Id}", newCar);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<CarsController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Car> Put(int id, [FromBody] Car value)
        {
            var test = _repo.UpdateCar(id, value);
            if (test == null) return NotFound("No cars with this ID " + id);
            return Ok(test);
        }

        // DELETE api/<CarsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            var deleted = _repo.RemoveCar(id);
            if (deleted == null) return NotFound("No cars with this ID " + id);
            return Ok("Car with ID " + id + " has been deleted by an admin!");
        }
    }
}
