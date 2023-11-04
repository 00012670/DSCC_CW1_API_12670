using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitController : ControllerBase
    {
        private readonly IHabitRepository _habitRepository;

        // Constructor to initialize the controller with a specific repository
        public HabitController(IHabitRepository habitRepository)
        {
            _habitRepository = habitRepository;
        }

        // Method to handle successful operations within a transaction scope
        private ActionResult HandleSuccessfulOperation(object? result)
        {
            // Using a TransactionScope to ensure atomicity of the operation
            using var scope = new TransactionScope();
            scope.Complete(); // Marking the transaction as complete
            return Ok(result); // Returning an HTTP 200 OK response with the result
        }

        // HTTP GET method to retrieve all habits
        [HttpGet]
        public IActionResult Get()
        {
            var habits = _habitRepository.GetHabits();
            return new OkObjectResult(habits);
        }

        // HTTP GET method to retrieve a habit by its ID
        [HttpGet, Route("{id}", Name = "GetHabitByID")]
        public IActionResult Get(int id)
        {
            var habit = _habitRepository.GetHabitById(id);
            return new OkObjectResult(habit);
        }

        // HTTP POST method to create a new habit
        [HttpPost]
        public ActionResult Post([FromBody] Habit habit)
        {
            _habitRepository.InsertHabit(habit); // Inserting the new habit
            return HandleSuccessfulOperation(habit); 
        }

        // HTTP PUT method to update an existing habit by its ID
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Habit habit)
        {
            var existingHabit = _habitRepository.GetHabitById(id); // Getting the existing habit by ID

            // Checking if the habit with the specified ID exists
            if (existingHabit != null)
            {
                _habitRepository.UpdateHabit(habit); // Updating the habit
                return HandleSuccessfulOperation(null);
            }

            return NotFound(); // Returning a 404 Not Found response if the habit doesn't exist
        }

        // HTTP DELETE method to delete a habit by its ID
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _habitRepository.DeleteHabit(id); 
            return HandleSuccessfulOperation(null);
        }
    }
}
