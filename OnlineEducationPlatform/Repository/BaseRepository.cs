using System.Linq.Expressions;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace OnlineEducationPlatform.Repository;

public class BaseRepository<T> : IRepository<T> where T : class

{
    protected readonly OnlineEducationDbContext _context;
    protected readonly DbSet<T> _dbSet;
    public BaseRepository(OnlineEducationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public DbSet<T> GetDbSet()
    {
        return _dbSet;
    }
    public List<T> Get()
    {
        return _dbSet.ToList();
    }

    public List<A> Get<A>() where A : class
    {
        return null;
    }

    public List<T> GetWhere(Expression<Func<T, bool>> metot)
    {
        return _dbSet.Where(metot).ToList();
    }

    public List<A> GetWhere<A>(Expression<Func<A, bool>> metot) where A : class
    {
        throw new NotImplementedException();
    }

    public T GetSingle(Func<T, bool> metot)
    {
        return _dbSet.SingleOrDefault(metot);
    }

    public A GetSingle<A>(Func<A, bool> metot) where A : class
    {
        throw new NotImplementedException();
    }

    public T GetById(int id)
    {
        return _dbSet.Find(id);
    }


    public A GetById<A>(int id) where A : class
    {
        throw new NotImplementedException();
    }

    public bool Add(T model)
    {
        EntityEntry<T> entityEntry = _dbSet.Add(model);
        return entityEntry.State == EntityState.Added;
    }

    public bool Add<A>(A model) where A : class
    {
        throw new NotImplementedException();
    }

    public bool Remove(T model)
    {
        throw new NotImplementedException();
    }

    public bool Remove<A>(A model) where A : class
    {
        throw new NotImplementedException();
    }

    public bool Remove(int id)
    {
        throw new NotImplementedException();
    }

    public bool Remove<A>(int id) where A : class
    {
        throw new NotImplementedException();
    }

    public bool Update(T model, int id)
    {
        throw new NotImplementedException();
    }

    public bool Update<A>(A model, int id) where A : class
    {
        throw new NotImplementedException();
    }

    public int Save()
    {
        return _context.SaveChanges();
    }
}