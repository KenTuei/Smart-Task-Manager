namespace SmartTaskPro.API.Models
{
    public class TaskItem
    {
        public int Id { get; set; } // Primary key
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DueDate { get; set; }

        // Foreign key — each task belongs to one user
        public int UserId { get; set; }
        public User? User { get; set; } // Navigatio
    }
}
