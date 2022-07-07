using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieMVC.Repositories.Interfaces;
using MovieMVC.Data;
using Microsoft.EntityFrameworkCore;

namespace MovieMVC.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected MovieContext context;
        protected DbSet<T> dbSet;
        
        public Repository(MovieContext movieContext)
        {
            context = movieContext;
            dbSet = context.Set<T>();
        }

        public virtual T Delete(T entity)
        {
            dbSet.Remove(entity);
            context.SaveChanges();

            return entity;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public virtual T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual T Insert(T entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();

            return entity;
        }

        public virtual T Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();

            return entity;
        }
    }
}
