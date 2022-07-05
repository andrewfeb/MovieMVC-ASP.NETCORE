using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieMVC.Repositories.Interfaces;
using MovieMVC.Models;
using MovieMVC.Data;

namespace MovieMVC.Repositories
{
    public class GenreRepository:Repository<Genre>, IGenreRepository
    {
        public GenreRepository(MovieContext context):base(context)
        { }
    }
}
