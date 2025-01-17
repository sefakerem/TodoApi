using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoAPI.Data;
using TodoAPI.DTOs;
using TodoAPI.Models;

namespace TodoAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TodoController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoDto>>> GetTodos()
    {
        var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value!);
        var todos = await _context.Todos
            .Where(t => t.UserId == userId)
            .Select(t => new TodoDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                IsCompleted = t.IsCompleted,
                CreatedAt = t.CreatedAt
            })
            .ToListAsync();

        return Ok(todos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoDto>> GetTodo(int id)
    {
        var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value!);
        var todo = await _context.Todos
            .Where(t => t.UserId == userId && t.Id == id)
            .Select(t => new TodoDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                IsCompleted = t.IsCompleted,
                CreatedAt = t.CreatedAt
            })
            .FirstOrDefaultAsync();

        if (todo == null)
            return NotFound();

        return Ok(todo);
    }

    [HttpPost]
    public async Task<ActionResult<TodoDto>> CreateTodo(CreateTodoDto todoDto)
    {
        var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value!);
        var todo = new Todo
        {
            Title = todoDto.Title,
            Description = todoDto.Description,
            CreatedAt = DateTime.UtcNow,
            UserId = userId
        };

        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTodo), new { id = todo.Id }, new TodoDto
        {
            Id = todo.Id,
            Title = todo.Title,
            Description = todo.Description,
            IsCompleted = todo.IsCompleted,
            CreatedAt = todo.CreatedAt
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodo(int id, CreateTodoDto todoDto)
    {
        var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value!);
        var todo = await _context.Todos.FirstOrDefaultAsync(t => t.UserId == userId && t.Id == id);

        if (todo == null)
            return NotFound();

        todo.Title = todoDto.Title;
        todo.Description = todoDto.Description;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("{id}/complete")]
    public async Task<IActionResult> CompleteTodo(int id)
    {
        var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value!);
        var todo = await _context.Todos.FirstOrDefaultAsync(t => t.UserId == userId && t.Id == id);

        if (todo == null)
            return NotFound();

        todo.IsCompleted = !todo.IsCompleted;
        await _context.SaveChangesAsync();
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo(int id)
    {
        var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value!);
        var todo = await _context.Todos.FirstOrDefaultAsync(t => t.UserId == userId && t.Id == id);

        if (todo == null)
            return NotFound();

        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
