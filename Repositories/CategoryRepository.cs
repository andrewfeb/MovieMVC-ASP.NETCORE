using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieMVC.Models;
using MovieMVC.Repositories.Interfaces;
using MovieMVC.Data;
using Microsoft.EntityFrameworkCore;

namespace MovieMVC.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(MovieContext context) : base(context)
        {
        }

        public Category GetDetail(int id)
        {
            //eager loading
            return dbSet.Where(c => c.Id == id).Include(c => c.Movies).FirstOrDefault();
        }

    }
}
