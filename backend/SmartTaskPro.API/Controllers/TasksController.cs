using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartTaskPro.API.Data;
using SmartTaskPro.API.Models;
using SmartTaskPro.API.Models.DTOs;

namespace SmartTaskPro.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasks()
        {
            var tasks = await _context.Tasks
                .Include(t => t.User)
                .Select(t => new TaskDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    IsCompleted = t.IsCompleted,
                    CreatedAt = t.CreatedAt,
                    DueDate = t.DueDate ?? default(DateTime),
                    UserId = t.UserId,
                    User = t.User != null ? new UserSimpleDto
                    {
                        Id = t.User.Id,
                        Name = t.User.Name,
                        Email = t.User.Email,
                        Role = t.User.Role
                    } : null
                })
                .ToListAsync();

            return Ok(tasks);
        }

        // GET: api/Tasks/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetTask(int id)
        {
            var task = await _context.Tasks
                .Include(t => t.User)
                .Where(t => t.Id == id)
                .Select(t => new TaskDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    IsCompleted = t.IsCompleted,
                    CreatedAt = t.CreatedAt,
                    DueDate = t.DueDate ?? default(DateTime),
                    UserId = t.UserId,
                    User = t.User != null ? new UserSimpleDto
                    {
                        Id = t.User.Id,
                        Name = t.User.Name,
                        Email = t.User.Email,
                        Role = t.User.Role
                    } : null
                })
                .FirstOrDefaultAsync();

            if (task == null)
                return NotFound();

            return Ok(task);
        }

        // POST: api/Tasks
        [HttpPost]
        public async Task<ActionResult<TaskDto>> CreateTask(TaskDto taskDto)
        {
            var task = new TaskItem
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                IsCompleted = taskDto.IsCompleted,
                CreatedAt = taskDto.CreatedAt,
                DueDate = taskDto.DueDate,
                UserId = taskDto.UserId
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            var user = await _context.Users.FindAsync(task.UserId);

            var resultDto = new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                CreatedAt = task.CreatedAt,
                DueDate = task.DueDate ?? default(DateTime),
                UserId = task.UserId,
                User = user != null ? new UserSimpleDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Role = user.Role
                } : null
            };

            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, resultDto);
        }

        // PUT: api/Tasks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskDto taskDto)
        {
            if (id != taskDto.Id)
                return BadRequest();

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return NotFound();

            task.Title = taskDto.Title;
            task.Description = taskDto.Description;
            task.IsCompleted = taskDto.IsCompleted;
            task.CreatedAt = taskDto.CreatedAt;
            task.DueDate = taskDto.DueDate;
            task.UserId = taskDto.UserId;

            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Tasks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
