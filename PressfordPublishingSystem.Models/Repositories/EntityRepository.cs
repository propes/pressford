using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressfordPublishingSystem.Models
{
    public class EntityRepository<T> : IEntityRepository<T> where T : class
    {
        protected DbContext _db;

        public EntityRepository(DbContext db)
        {
            _db = db;
        }

        public virtual void Create(T entity)
        {
            _db.Set<T>().Add(entity);
            _db.SaveChanges();
        }

        public virtual IQueryable<T> Read()
        {
            return _db.Set<T>();
        }

        public virtual T Find(int id)
        {
            return _db.Set<T>().Find(id);
        }

        public virtual void Update(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            _db.Entry(entity).State = EntityState.Deleted;
            _db.SaveChanges();
        }

        public void Dispose()
        {
            if (_db != null)
                _db.Dispose();
        }
    }
}
