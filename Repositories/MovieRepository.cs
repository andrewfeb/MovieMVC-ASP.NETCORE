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

        public override async Task<IEnumerable<Movie>> GetAll()
        {
            return await dbSet.Include(m => m.Category).ToListAsync();
        }

        public async Task<Movie> GetDetail(int id)
        {
            return await dbSet.Where(m => m.Id == id)
                        .Include(m => m.Category)
                        .Include(m => m.Genres)
                        .FirstOrDefaultAsync();
        }

        public override async Task<Movie> Insert(Movie entity)
        {
            context.Genres.AttachRange(entity.Genres);
            dbSet.Add(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public override async Task<Movie> Update(Movie entity)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    await context.Database.ExecuteSqlRawAsync("Delete FROM GenreMovie WHERE MoviesId= {0}", entity.Id);
                    context.Genres.AttachRange(entity.Genres);
                    context.Entry(entity).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }catch(Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception(ex.Message);
                }
            }
            return entity;
        }
    }
}
