using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TodoApi.Core;

namespace TodoApi.Application.Features.Todos.Commands
{
    public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, TodoItem>
    {
        private readonly ITodoRepository _repository;

        public CreateTodoCommandHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<TodoItem> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            var todoItem = new TodoItem
            {
                Title = request.Title,
                Description = request.Description,
                CreatedDate = DateTime.UtcNow
            };

            await _repository.AddAsync(todoItem);
            await _repository.SaveChangesAsync();

            return todoItem;
        }
    }
}