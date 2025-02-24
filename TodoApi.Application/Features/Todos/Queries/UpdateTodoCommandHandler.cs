using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TodoApi.Core;

namespace TodoApi.Application.Features.Todos.Queries
{
    public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand, Unit>
    {
        private readonly ITodoRepository _repository;

        public UpdateTodoCommandHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            var todoItem = await _repository.GetByIdAsync(request.Id);
            if (todoItem == null)
            {
                throw new NotFoundException(nameof(TodoItem), request.Id);
            }

            todoItem.Title = request.Title;
            todoItem.Description = request.Description;
            todoItem.IsCompleted = request.IsCompleted;

            await _repository.UpdateAsync(todoItem);
            return Unit.Value;
        }
    }
}