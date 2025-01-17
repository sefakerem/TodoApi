namespace TodoAPI.DTOs;

public class CreateTodoDto
{
    public required string Title { get; set; }
    public string Description { get; set; } = string.Empty;
}

public class TodoDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
}
