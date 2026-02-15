using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Models;
using TaskManagementAPI.DTOs;
using TaskManagementAPI.Data;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllTasks()
        {
            var response = _context.Tasks.Select(task => new TaskResponseDto
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
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);

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
            taskObj.Title = newTask.Title;
            taskObj.IsCompleted = newTask.IsCompleted;
            taskObj.Description = newTask.Description;
            taskObj.CreatedDate = DateTime.Now;
            _context.Tasks.Add(taskObj);
            _context.SaveChanges();

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
            var existingTask = _context.Tasks.FirstOrDefault(t => t.Id == id);

            if (existingTask != null)
            {
                existingTask.Title = updatedTask.Title;
                existingTask.Description = updatedTask.Description;
                existingTask.IsCompleted = updatedTask.IsCompleted;
                _context.SaveChanges();
                return NoContent();
            }
            else
                return NotFound();
        }

        [HttpDelete("{id}")]
        
        public IActionResult DeleteTask(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
                return NoContent();
            }
            else 
                return NotFound();

        }
    }
}
