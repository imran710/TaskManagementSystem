using TaskManagement.Domain.Enums;

namespace TaskManagement.Domain.Entities;
public class User
{
    public int Id { get; set; }
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public UserRole Role { get; set; }

    public ICollection<TaskItem> AssignedTasks { get; set; } = new List<TaskItem>();
}
