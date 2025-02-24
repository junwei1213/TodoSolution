using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApi.Core;
using TodoApi.Infrastructure.Data;

namespace TodoApi.Infrastructure.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;

        public TodoRepository(TodoContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TodoItem item) =>
            await _context.TodoItems.AddAsync(item);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
        public async Task<IEnumerable<TodoItem>> GetAllAsync() =>
            await _context.TodoItems.ToListAsync();

        public async Task<TodoItem> GetByIdAsync(int id) =>
            await _context.TodoItems.FindAsync(id);

        public async Task UpdateAsync(TodoItem item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if (item != null)
            {
                _context.TodoItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}