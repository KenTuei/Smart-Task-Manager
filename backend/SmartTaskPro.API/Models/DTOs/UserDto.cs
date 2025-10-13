namespace SmartTaskPro.API.Models.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Role { get; set; } = "";
        // Optional: Include task titles only
        public List<string> TaskTitles { get; set; } = new();
    }
}
