using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Core;

namespace TodoApi.Application.Features.Todos.Commands
{
    public class CreateTodoCommand : IRequest<TodoItem>, IBaseRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
