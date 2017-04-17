using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressfordPublishingSystem.Models
{
    public interface IEntityRepository<T> : IDisposable where T : class
    {
        void Create(T entity);

        IQueryable<T> Read();

        T Find(int id);

        void Update(T entity);

        void Delete(T entity);
    }
}
