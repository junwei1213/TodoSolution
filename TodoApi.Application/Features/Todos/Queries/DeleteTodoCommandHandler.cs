using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TodoApi.Core;

namespace TodoApi.Application.Features.Todos.Queries
{
    public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand, Unit>
    {
        private readonly ITodoRepository _repository;

        public DeleteTodoCommandHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            var todoItem = await _repository.GetByIdAsync(request.Id);
            if (todoItem == null)
            {
                throw new NotFoundException(nameof(TodoItem), request.Id);
            }

            await _repository.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}