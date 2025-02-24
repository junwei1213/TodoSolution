using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApi.Application.Features.Todos.Queries
{
    public class DeleteTodoCommand : IRequest<Unit>, IBaseRequest
    {
        public int Id { get; set; }

        public DeleteTodoCommand(int id)
        {
            Id = id;
        }
    }
}
