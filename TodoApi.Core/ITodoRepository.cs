using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApi.Core
{
    public interface ITodoRepository
    {
        Task<TodoItem> GetByIdAsync(int id);
        Task<IEnumerable<TodoItem>> GetAllAsync();
        Task AddAsync(TodoItem item);
        Task SaveChangesAsync();
        Task UpdateAsync(TodoItem item);
        Task DeleteAsync(int id);

    }
}
