using Cinema_project_MVC.Data;
using Cinema_project_MVC.Repository.IReprsitory;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cinema_project_MVC.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        AppDbContext db;//= new AplicationDbContext();
        DbSet<T> dbset;
        public Repository(AppDbContext  db)
        {
            this.db = db;
            dbset = db.Set<T>();
        }

        public IEnumerable<T> GetAll(

    Func<IQueryable<T>, IQueryable<T>>[]? includes = null,
    Expression<Func<T, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<T> query = dbset;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = include(query);
                }
            }


            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            return query.ToList();
        }



        public T? Getone(Func<IQueryable<T>, IQueryable<T>>[]? includes = null,
    Expression<Func<T, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<T> query = dbset;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = include(query);
                }
            }


            if (filter != null)
            {
                query = query.Where(filter);
            }
            //return query.ToList();
            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            return query.FirstOrDefault();


        }
        public void Add(T entity)
        {

            dbset.Add(entity);

        }

        public void Edit(T entity)
        {
            dbset.Update(entity);
        }
        public void Delete(T entity)
        {
            dbset.Remove(entity);
        }
        public void Commit()
        {
            db.SaveChanges();
        }
    }
}
