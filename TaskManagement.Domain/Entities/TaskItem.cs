namespace TaskManagement.Domain.Entities;
public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public TaskStatus Status { get; set; } = TaskStatus.WaitingToRun;

    public int AssignedToUserId { get; set; }
    public User AssignedToUser { get; set; } = default!;

    public int CreatedByUserId { get; set; }

    public int TeamId { get; set; }
    public Team Team { get; set; } = default!;

    public DateTime DueDate { get; set; }
}