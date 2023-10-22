using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DrGreiche.DataAccess.Repository.IRepository;
using DrGreiche.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DrGreiche.DataAccess.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<TEntity> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<TEntity>();
            
        }
        
        public TEntity Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        public void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            dbSet.Remove(entity);
        }
    }
}
