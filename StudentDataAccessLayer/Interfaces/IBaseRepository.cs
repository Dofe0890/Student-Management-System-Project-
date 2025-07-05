using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
namespace StudentDomainLayer.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> GetByIDAsync(int id);
        Task<IEnumerable <T>> GetAll();
        Task<T> AddRecordAsync(T entity);
        Task<bool> DeleteByIdAsync(int id);
        Task<bool> UpdateAsync(T entity);
        Task<T> Find(Expression<Func<T, bool>> criteria);
        Task<IEnumerable <T>> FindAll(Expression<Func<T, bool>> criteria ,int? skip , int? take );
        Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> criteria, int? skip, int? take,Expression<Func<T,object>> orderBy = null , string OrderByDirection = "ASC" );

    }
}
