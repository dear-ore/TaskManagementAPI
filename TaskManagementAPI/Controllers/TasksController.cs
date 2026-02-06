using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private static List<TaskItem> MyTasks = new List<TaskItem>
        {
            new TaskItem { Id = 1, Title = "Dishes", Description = "Do the dishes before 4pm", IsCompleted = false, CreatedDate = DateTime.Now },
            new TaskItem { Id = 2, Title = "Cleaning", Description = "Clean the house", IsCompleted = false, CreatedDate = DateTime.Now }
        };

        [HttpGet]
        public IActionResult GetAllTasks()
        {
            return Ok(MyTasks);
        }

        [HttpGet("{id}")]
        public IActionResult GetTaskById(int id)
        {
            var task = MyTasks.FirstOrDefault(t => t.Id == id);
            if(task != null)
            {
                return Ok(task);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
