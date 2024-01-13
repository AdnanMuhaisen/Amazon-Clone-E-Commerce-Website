using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace amazon_clone.DataAccess.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Remove(T entity);
        T? GetFirst(string IncludeProperties = null!);
        T? GetFirstAsNoTracking(string IncludeProperties = null!);
        T? Get(Expression<Func<T, bool>> filter, string IncludeProperties = null!);
        IEnumerable<T>? GetAll(Expression<Func<T, bool>> filter = null!,string IncludeProperties = null!);
        T? GetAsNoTracking(Expression<Func<T, bool>> filter, string IncludeProperties = null!);
        IEnumerable<T>? GetAllAsNoTracking(Expression<Func<T, bool>> filter = null!,string IncludeProperties = null!);
        
        // to include indivisual property and then include the inner property.
        T? Get(Func<IQueryable<T>, IIncludableQueryable<T, object>> include, Expression<Func<T, bool>> filter = null!);
        IEnumerable<T>? GetAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> include, Expression<Func<T, bool>> filter = null!);
        T? GetAsNoTracking(Func<IQueryable<T>, IIncludableQueryable<T, object>> include, Expression<Func<T, bool>> filter = null!);
        IEnumerable<T>? GetAllAsNoTracking(Func<IQueryable<T>, IIncludableQueryable<T, object>> include, Expression<Func<T, bool>> filter = null!);
    }
}
