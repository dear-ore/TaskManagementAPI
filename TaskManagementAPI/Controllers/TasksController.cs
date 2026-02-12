using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Models;
using TaskManagementAPI.DTOs;

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
            var response = MyTasks.Select(task => new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                CreatedDate = task.CreatedDate,
            }).ToList();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetTaskById(int id)
        {
            var task = MyTasks.FirstOrDefault(t => t.Id == id);

            var response = new TaskResponseDto();

            if (task != null)
            {
                response.Id = task.Id;
                response.Title = task.Title;
                response.Description = task.Description;
                response.IsCompleted = task.IsCompleted;
                response.CreatedDate = task.CreatedDate;
                return Ok(response);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult CreateTasks([FromBody] CreateTaskDto newTask)
        {
            TaskItem taskObj = new TaskItem();
            taskObj.Id = MyTasks.Any() ? MyTasks.Max(t => t.Id) + 1 : 1;

            taskObj.Title = newTask.Title;
            taskObj.IsCompleted = newTask.IsCompleted;
            taskObj.Description = newTask.Description;
            taskObj.CreatedDate = DateTime.Now;
            MyTasks.Add(taskObj);

            TaskResponseDto response = new TaskResponseDto();
            response.Id = taskObj.Id;
            response.Title = taskObj.Title;
            response.Description = taskObj.Description;
            response.IsCompleted = taskObj.IsCompleted;
            response.CreatedDate = taskObj.CreatedDate;
            return CreatedAtAction(
                nameof(GetTaskById),
                new { id = response.Id },
                response
            );
        }

        [HttpPut("{id}")]

        public IActionResult UpdateTasks(int id, [FromBody] UpdateTaskDto updatedTask)
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
