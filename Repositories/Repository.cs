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
        private DbSet<T> dbSet;
        
        public Repository(MovieContext movieContext)
        {
            context = movieContext;
            dbSet = context.Set<T>();
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
            context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public void Insert(T entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
