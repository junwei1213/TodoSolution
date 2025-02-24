using MediatR;
using TodoApi.Core;

namespace TodoApi.Application.Features.Todos.Queries
{
    public class GetTodoItemsQuery : IRequest<IEnumerable<TodoItem>>
    {

    }
}