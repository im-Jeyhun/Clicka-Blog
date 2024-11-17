using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClickaholicWebApp.BLL.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> GetAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> whereCondition);
        Task<IEnumerable<T>> GetAllByCondition(Expression<Func<T, bool>> whereCondition);
        Task<T> GetByCondition(Expression<Func<T, bool>> whereCondition);
        Task<int> SaveChangesAsync();

    }
}
