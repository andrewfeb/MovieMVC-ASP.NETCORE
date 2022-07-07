using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieMVC.Models;

namespace MovieMVC.Repositories.Interfaces
{
    public interface IMovieRepository: IRepository<Movie>
    {
        Movie GetDetail(int id);
    }
}
