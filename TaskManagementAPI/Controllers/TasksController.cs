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
            new TaskItem { Id = 2, Title = "Cleaning", Description = "Clean the house", IsCompleted = false, CreatedDate = DateTime.Now },
            new TaskItem { Id = 3, Title = "Laundry", Description = "Do laundry before dusk", IsCompleted = true, CreatedDate = DateTime.Now }
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
            if (task != null)
            {
                return Ok(task);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult CreateTasks([FromBody] TaskItem newTask)
        {
            newTask.Id = MyTasks.Any() ? MyTasks.Max(t => t.Id) + 1 : 1;
            newTask.CreatedDate = DateTime.Now;
            MyTasks.Add(newTask);

            return CreatedAtAction(
                nameof(GetTaskById),
                new { id = newTask.Id },
                newTask
            );
        }

        [HttpPut("{id}")]

        public IActionResult UpdateTasks(int id, [FromBody] TaskItem updatedTask)
        {
            var existingTask = MyTasks.FirstOrDefault(t => t.Id == id);

            if (existingTask != null)
            {

                existingTask.Title = updatedTask.Title;
                existingTask.Description = updatedTask.Description;
                existingTask.IsCompleted = updatedTask.IsCompleted;

                return NoContent();
            }
            else
                return NotFound();
        }

        [HttpDelete("{id}")]
        
        public IActionResult DeleteTask(int id)
        {
            var taskToDelete = MyTasks.FirstOrDefault(t => t.Id == id);
            if (taskToDelete != null)
            {
                MyTasks.Remove(taskToDelete);
                return NoContent();
            }
            else 
                return NotFound();

        }
    }
}
