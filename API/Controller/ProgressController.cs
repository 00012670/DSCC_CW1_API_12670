using API.Models;        
using API.Repositories;    
using Microsoft.AspNetCore.Mvc;  
using System.Transactions;  

namespace API.Controllers
{
    // Defining the ProgressController class that inherits from ControllerBase
    [Route("api/[controller]")]    // Attribute specifying the route for the controller
    [ApiController]               
    public class ProgressController : ControllerBase
    {
        // Field to hold the IProgressRepository instance
        private readonly IProgressRepository _progressRepository;

        // Constructor to initialize the controller with a specific repository
        public ProgressController(IProgressRepository progressRepository)
        {
            _progressRepository = progressRepository;
        }

        // Method to handle a successful operation within a transaction scope
        private ActionResult HandleSuccessfulOperation(object? result)
        {
            // Using a TransactionScope to ensure atomicity of the operation
            using var scope = new TransactionScope();
            scope.Complete();
            return Ok(result);
        }

        // HTTP GET method to retrieve all progress records
        [HttpGet]
        public IActionResult Get()
        {
            // Retrieve all progress records from the repository
            var progress = _progressRepository.GetProgresses();

            // Return an OkObjectResult with the progress records
            return new OkObjectResult(progress);
        }

        // HTTP GET method to retrieve a progress record by ID
        [HttpGet("{id}", Name = "GetProgressByID")]
        public IActionResult Get(int id)
        {
            // Retrieve a progress record by ID from the repository
            var progress = _progressRepository.GetProgressById(id);

            // Return an OkObjectResult with the retrieved progress record
            return new OkObjectResult(progress);
        }

        // HTTP POST method to create a new progress record
        [HttpPost]
        public ActionResult Post([FromBody] Progress progress)
        {
            // Insert the new progress record into the repository
            _progressRepository.InsertProgress(progress);
            return HandleSuccessfulOperation(progress);
        }

        // HTTP PUT method to update an existing progress record by ID
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Progress progress)
        {
            // Retrieve the existing progress record by ID from the repository
            var existingProgress = _progressRepository.GetProgressById(id);

            // If the existing progress record is found, update it in the repository
            if (existingProgress != null)
            {
                _progressRepository.UpdateProgress(progress);
                return HandleSuccessfulOperation(null);
            }

            // If the existing progress record is not found, return NotFound
            return NotFound();
        }

        // HTTP DELETE method to delete a progress record by ID
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Delete the progress record by ID from the repository
            _progressRepository.DeleteProgress(id);
            return HandleSuccessfulOperation(null);
        }
    }
}
