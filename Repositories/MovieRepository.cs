using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieMVC.Repositories.Interfaces;
using MovieMVC.Models;
using MovieMVC.Data;
using Microsoft.EntityFrameworkCore;

namespace MovieMVC.Repositories
{
    public class MovieRepository:Repository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieContext context):base(context)
        { }

        public override IEnumerable<Movie> GetAll()
        {
            return dbSet.Include(m => m.Category).ToList();
        }

        public Movie GetDetail(int id)
        {
            return dbSet.Where(m => m.Id == id)
                        .Include(m => m.Category)
                        .Include(m => m.Genres)
                        .FirstOrDefault();
        }

        public override Movie Insert(Movie entity)
        {
            context.Genres.AttachRange(entity.Genres);
            dbSet.Add(entity);
            context.SaveChanges();

            return entity;
        }

        public override Movie Update(Movie entity)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Database.ExecuteSqlRaw("Delete FROM GenreMovie WHERE MoviesId= {0}", entity.Id);
                    context.Genres.AttachRange(entity.Genres);
                    context.Entry(entity).State = EntityState.Modified;
                    context.SaveChanges();
                    transaction.Commit();
                }catch(Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
            return entity;
        }
    }
}
