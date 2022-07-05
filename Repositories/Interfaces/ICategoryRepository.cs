using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieMVC.Models;

namespace MovieMVC.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();
        Category GetCategory(int  id);
        void InsertCateory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }
}
