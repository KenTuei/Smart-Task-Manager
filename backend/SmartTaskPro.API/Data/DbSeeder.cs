using Bogus;
using SmartTaskPro.API.Data;
using SmartTaskPro.API.Models;

public static class DbSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (!context.Users.Any())
        {
            var userFaker = new Faker<User>()
                .RuleFor(u => u.Name, f => f.Name.FullName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Role, f => f.PickRandom(new[] { "Admin", "Member", "Guest" }));

            var users = userFaker.Generate(10); // generate 10 users
            context.Users.AddRange(users);
            context.SaveChanges();

            var taskFaker = new Faker<TaskItem>()
                .RuleFor(t => t.Title, f => f.Lorem.Sentence(3))
                .RuleFor(t => t.Description, f => f.Lorem.Paragraph())
                .RuleFor(t => t.IsCompleted, f => f.Random.Bool())
                .RuleFor(t => t.CreatedAt, f => f.Date.Past())
                .RuleFor(t => t.DueDate, f => f.Date.Future())
                .RuleFor(t => t.UserId, f => f.PickRandom(users).Id);

            var tasks = taskFaker.Generate(30); // generate 30 tasks
            context.Tasks.AddRange(tasks);
            context.SaveChanges();
        }
    }
}
