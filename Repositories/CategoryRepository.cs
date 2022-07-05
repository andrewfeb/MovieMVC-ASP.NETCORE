using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieMVC.Models;
using MovieMVC.Repositories.Interfaces;
using MovieMVC.Data;

namespace MovieMVC.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(MovieContext context) : base(context)
        {
        }

    }
}
