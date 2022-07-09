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

        public virtual async Task<T> Delete(T entity)
        {
            dbSet.Remove(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<T> Insert(T entity)
        {
            dbSet.Add(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<T> Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return entity;
        }
    }
}
