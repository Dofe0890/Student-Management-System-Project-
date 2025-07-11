using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
namespace StudentDataAccessLayer.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> GetByIDAsync(int id);
        Task<IEnumerable <T>> GetAllAsync();
        Task<T> AddRecordAsync(T entity);
        Task<bool> DeleteByIdAsync(int id);
        Task<bool> UpdateAsync(T entity);
        Task<T> FindAsync(Expression<Func<T, bool>> criteria);
        Task<IEnumerable <T>> FindAllAsync(Expression<Func<T, bool>>? criteria ,int? skip , int? take );
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? skip, int? take,Expression<Func<T,object>> orderBy = null , string OrderByDirection = "ASC" );
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<int> Count(int id);
        Task<int> Count(Expression<Func<T, bool>> predicate);
        IQueryable<T> Query();
        Task<List<T>> ToListAsync();

    }
}
