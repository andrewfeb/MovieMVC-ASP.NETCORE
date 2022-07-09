using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieMVC.Models;

namespace MovieMVC.Repositories.Interfaces
{
    public interface ICategoryRepository:IRepository<Category>
    {
        Task<Category> GetDetail(int id);
    }
}
