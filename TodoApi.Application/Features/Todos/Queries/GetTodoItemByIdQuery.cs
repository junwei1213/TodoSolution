using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Core;

namespace TodoApi.Application.Features.Todos.Queries
{
    public class GetTodoItemByIdQuery : IRequest<TodoItem>
    {
        public int Id { get; set; }

        public GetTodoItemByIdQuery(int id)
        {
            Id = id;
        }
    }
}
