using Neureveal.Data.Models;
using Neureveal.Data.Repositories.GenericRepo;

namespace Neureveal.Data.Repositories.CategoryRepo
{
    public interface ICategoryRepo: IGenericRepo<Category>
    {
        public List<Category> GetCategoriesByIds(List<Guid> categoriesIds);
        public List<Category> GetAllIncludeArticles();
    }
}
