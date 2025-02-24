using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TodoApi.Core;

namespace TodoApi.Application.Features.Todos.Queries
{
    public class GetTodoItemByIdQueryHandler : IRequestHandler<GetTodoItemByIdQuery, TodoItem>
    {
        private readonly ITodoRepository _repository;

        public GetTodoItemByIdQueryHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<TodoItem> Handle(GetTodoItemByIdQuery request, CancellationToken cancellationToken)
        {
            var todoItem = await _repository.GetByIdAsync(request.Id);
            if (todoItem == null)
            {
                throw new NotFoundException(nameof(TodoItem), request.Id);
            }

            return todoItem;
        }
    }
}