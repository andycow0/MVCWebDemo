using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Domain.Models;
using System.Data.Entity;

namespace DataAccessLayer.Repository
{
    /// <summary>
    /// Generic Repository Patterm class
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private NORTHWNDEntities db;
        private DbSet<TEntity> dbSet;
        public GenericRepository()
        {
            db = new NORTHWNDEntities();
            dbSet = db.Set<TEntity>();
        }
        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = dbSet;


            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                return orderBy(query);
            else
                return query;
        }
        public TEntity Get(object id)
        {
            return dbSet.Find(id);
        }

        public void Delete(TEntity entity)
        {
            if (db.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
            db.SaveChanges();
        }

        public void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
            db.SaveChanges();
        }

        public void Insert(TEntity entity)
        {
            dbSet.Add(entity);
            db.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
