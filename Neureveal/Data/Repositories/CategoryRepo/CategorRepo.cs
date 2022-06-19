using Microsoft.EntityFrameworkCore;
using Neureveal.Data.Context;
using Neureveal.Data.Models;
using Neureveal.Data.Repositories.GenericRepo;

namespace Neureveal.Data.Repositories.CategoryRepo
{
    public class CategoryRepo : GenericRepo<Category>, ICategoryRepo
    {
        private readonly NewspaperContext context;
        public CategoryRepo(NewspaperContext _context) : base(_context)
        {
            context= _context;
        }

        public NewspaperContext Context { get; }

        public List<Category> GetCategoriesByIds(List<Guid> categoriesIds)
        {
            return context.Categories.Where(c=>categoriesIds.Contains(c.Id)).ToList();
        }
        public List<Category> GetAllIncludeArticles()
        {
            return context.Categories.Include(a => a.Articles).ToList();
        }
    }
}
