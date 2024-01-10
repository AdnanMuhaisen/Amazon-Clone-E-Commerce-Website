using amazon_clone.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace amazon_clone.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public AppDbContext _context { get; set; }
        public DbSet<T> dbSet { get; set; }

        public Repository(AppDbContext context)
        {
            _context = context;
            this.dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public T? Get(Expression<Func<T, bool>> filter, string IncludeProperties = null!)
        {
            IQueryable<T> _dbSet = dbSet;
            if (IncludeProperties is not null)
            {
                foreach (var type in IncludeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    _dbSet = _dbSet.Include(type.Trim());
                }
            }
            return _dbSet.FirstOrDefault(filter);
        }

        public IEnumerable<T> GetAll(string IncludeProperties = null!)
        {
            IQueryable<T> _dbSet = dbSet;
            if (IncludeProperties is not null)
            {
                foreach (var type in IncludeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    _dbSet = _dbSet.Include(type.Trim());
                }
            }
            return _dbSet;
        }

        public T? GetAsNoTracking(Expression<Func<T, bool>> filter, string IncludeProperties = null!)
        {
            IQueryable<T> _dbSet = dbSet;
            if (IncludeProperties is not null)
            {
                foreach (var type in IncludeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    _dbSet = _dbSet.Include(type.Trim());
                }
            }
            return _dbSet.AsNoTracking().FirstOrDefault(filter);
        }

        public T? GetFirst(string IncludeProperties = null!)
        {
            IQueryable<T> _dbSet = dbSet;
            if (IncludeProperties is not null)
            {
                foreach (var type in IncludeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    _dbSet = _dbSet.Include(type.Trim());
                }
            }
            return _dbSet.FirstOrDefault();
        }

        public IEnumerable<T>? GetAll(Expression<Func<T, bool>> filter = null!, string IncludeProperties = null!)
        {
            IQueryable<T> _dbSet = dbSet;
            if (IncludeProperties is not null)
            {
                foreach (var type in IncludeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    _dbSet = _dbSet.Include(type.Trim());
                }
            }
            if (filter is null)
                return _dbSet.ToList();

            return _dbSet.Where(filter).ToList();
        }

        public IEnumerable<T>? GetAllAsNoTracking(Expression<Func<T, bool>> filter = null!, string IncludeProperties = null!)
        {
            IQueryable<T> _dbSet = dbSet;
            if (IncludeProperties is not null)
            {
                foreach (var type in IncludeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    _dbSet = _dbSet.Include(type.Trim());
                }
            }

            if (filter is null)
                return _dbSet.AsNoTracking().ToList();

            return _dbSet.Where(filter).AsNoTracking().ToList();
        }

        public T? GetFirstAsNoTracking(string IncludeProperties = null!)
        {
            IQueryable<T> _dbSet = dbSet;
            if (IncludeProperties is not null)
            {
                foreach (var type in IncludeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    _dbSet = _dbSet.Include(type.Trim());
                }
            }
            return _dbSet.AsNoTracking().FirstOrDefault();
        }
    }
}
