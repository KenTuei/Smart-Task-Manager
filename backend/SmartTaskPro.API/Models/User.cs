namespace SmartTaskPro.API.Models
{
    public class User
    {
        public int Id { get; set; } // Primary key
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = "User";

        // Navigation property — one user can have many tasks
        public List<TaskItem>? Tasks { get; set; }
    }
}
