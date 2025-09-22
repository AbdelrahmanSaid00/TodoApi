using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoApi.interfaces;
using TodoApi.Model;

namespace TodoApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private readonly IAuditService _auditService;

        public TodoController(ITodoService todoService, IAuditService auditService)
        {
            _todoService = todoService;
            _auditService = auditService;
        }
        [HttpPost("AddNew")]
        public async Task<IActionResult> AddNew (int id , Todo todo)
        {
            if(ModelState.IsValid)
            {
                await _todoService.AddAsync(todo);
                var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var ip = HttpContext.Connection.RemoteIpAddress.ToString();

                await _auditService.LogActionAsync(
                        UserId,
                        "Add",
                        $"{todo.Title} has been Added"
                        , ip
                    );
                return Created(string.Empty, todo);
            }
            return BadRequest(ModelState);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(Todo todo)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var ip = HttpContext.Connection.RemoteIpAddress.ToString();
                await _auditService.LogActionAsync(userId,
                    "Update",
                    $" {todo.Title} has been updated",
                    ip);
                await _todoService.UpdateAsync(todo);
                return Ok(todo);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var todo = await _todoService.GetByIdAsync(id);
            if (todo != null)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var ip = HttpContext.Connection.RemoteIpAddress.ToString();
                await _auditService.LogActionAsync(userId,
                    "Delete",
                    $" {todo.Title} has been deleted",
                    ip);

                await _todoService.DeleteAsync(id);
                return NoContent();
            }
            ModelState.AddModelError(String.Empty, "Todo Not Found");
            return BadRequest(ModelState);
        }
        [HttpGet ("GetAll")]
        public async Task<IActionResult> GetTodoItems(int page = 1, int pageSize = 10)
        {
            var total = (await _todoService.GetAllAsync()).Count();
            var todos = (await _todoService.GetAllAsync())
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);
            if (todos != null)
                return Ok(new
                {
                    todos,
                    page,
                    pageSize,
                    total
                });
            return NoContent();
        }
    }
}
