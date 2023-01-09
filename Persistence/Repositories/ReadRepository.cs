using CRUDOperation.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : class
    {
        private readonly CRUDOperationsDbContext _context;

        public ReadRepository(CRUDOperationsDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll() => Table;

        public async Task<T> GetByIdAsync(int id) => await Table.FindAsync(id);

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> filter) => await Table.FirstOrDefaultAsync(filter);

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> filter) => Table.Where(filter);
    }
}
