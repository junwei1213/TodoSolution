using Microsoft.AspNetCore.Mvc;
using MediatR;
using TodoApi.Application.Features.Todos.Queries;
using TodoApi.Core;
using TodoApi.Application.Features.Todos.Commands;

namespace TodoApi.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodosController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TodosController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodos()
        {
            return Ok(await _mediator.Send(new GetTodoItemsQuery()));
        }

        [HttpPost]
        [ProducesResponseType(typeof(TodoItem), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TodoItem>> CreateTodo(CreateTodoCommand command)
        {
            var createdTodo = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetTodos), new { id = createdTodo.Id }, createdTodo);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTodo(int id, UpdateTodoCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            var todo = await _mediator.Send(new GetTodoItemByIdQuery(id));
            if (todo == null)
            {
                return NotFound();
            }

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var todo = await _mediator.Send(new GetTodoItemByIdQuery(id));
            if (todo == null)
            {
                return NotFound();
            }

            await _mediator.Send(new DeleteTodoCommand(id));
            return NoContent();
        }
    }
}