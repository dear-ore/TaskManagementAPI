using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Controllers
{
    public class TasksController : Controller
    {
        private static List<TaskItem> MyTasks = new TaskItem
        {
            Id = 1,
            Title = "Dishes"
            Description = "Do the dishes before 4pm",
            IsCompleted = false,
            CreatedDate = DateTime.Now
        };       
    }
}
