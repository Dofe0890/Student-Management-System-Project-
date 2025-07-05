using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentDomainLayer.Interfaces;
using StudentDomainLayer.Models;

namespace StudentDomainLayer.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddRecordAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);


            if (entity == null)
            {
                return false;
            }
            _context.Set<T>().Remove(entity);

            return true;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIDAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await Task.CompletedTask;
            return true;
        }

        public async Task<T> Find(Expression<Func<T, bool>> criteria)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(criteria); 
        }

        public async Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> criteria, int? skip, int? take)
        {
            IQueryable<T> query = _context.Set<T>().Where(criteria);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> criteria, int? skip, int? take, Expression<Func<T, object>> orderBy = null, string OrderByDirection = "ASC")
        {
            IQueryable<T> query = _context.Set<T>().Where(criteria);

            if(skip.HasValue)
                query = query.Skip(skip.Value);
            if(take.HasValue)
                query = query.Take(take.Value);

            if(orderBy != null)
            {
                if(OrderByDirection == "ASC" )
                {
                    query = query.OrderBy(orderBy);
                }
                else
                {
                    query = query.OrderByDescending(orderBy);
                }
            }
            
            return await query.ToListAsync();
        }
    }
}
