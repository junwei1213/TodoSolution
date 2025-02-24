using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TodoApi.Core;
using TodoApi.Infrastructure.Repositories;

namespace TodoApi.Application.Features.Todos.Queries
{
    public class GetTodoItemsQueryHandler
        : IRequestHandler<GetTodoItemsQuery, IEnumerable<TodoItem>>
    {
        private readonly ITodoRepository _repository;

        public GetTodoItemsQueryHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TodoItem>> Handle(
            GetTodoItemsQuery request,
            CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}