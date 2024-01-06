using System.Linq.Expressions;

namespace amazon_clone.DataAccess.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Delete(T entity);
        T? Get(Expression<Func<T, bool>> filter, string IncludeProperties = null!);
        T? GetFirst(string IncludeProperties = null!);
        T? GetFirstAsNoTracking(string IncludeProperties = null!);
        T? GetAsNoTracking(Expression<Func<T, bool>> filter, string IncludeProperties = null!);
        IEnumerable<T>? GetAll(Expression<Func<T, bool>> filter = null!,string IncludeProperties = null!);
        IEnumerable<T>? GetAllAsNoTracking(Expression<Func<T, bool>> filter = null!,string IncludeProperties = null!);
    }
}
