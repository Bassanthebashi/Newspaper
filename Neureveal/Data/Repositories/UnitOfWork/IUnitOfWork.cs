
using Neureveal.Data.Repositories.ArticleRepo;
using Neureveal.Data.Repositories.CategoryRepo;

namespace Neureveal.Data.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IArticleRepo ArticleRepo { get; }
        public ICategoryRepo CategoryRepo { get; }
    }
}
