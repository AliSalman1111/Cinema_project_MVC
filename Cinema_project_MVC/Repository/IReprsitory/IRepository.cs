using System.Linq.Expressions;

namespace Cinema_project_MVC.Repository.IReprsitory
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll(

         Func<IQueryable<T>, IQueryable<T>>[]? includes = null,
         Expression<Func<T, bool>>? filter = null, bool tracked = true);
        T? Getone(Func<IQueryable<T>, IQueryable<T>>[]? includes = null,
         Expression<Func<T, bool>>? filter = null, bool tracked = true);

        void Add(T category);


        void Edit(T category);

        void Delete(T category);

        void Commit();

    }
}
