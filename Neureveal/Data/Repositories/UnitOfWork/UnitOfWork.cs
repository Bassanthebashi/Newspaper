using Neureveal.Data.Repositories.ArticleRepo;
using Neureveal.Data.Repositories.CategoryRepo;

namespace Neureveal.Data.Repositories.UnitOfWork
{
    public class UnitOfWork:IUnitOfWork
    {
        public UnitOfWork(IArticleRepo articleRepo,ICategoryRepo categoryRepo)
        {
            ArticleRepo=articleRepo;
            CategoryRepo=categoryRepo;
        }

        public IArticleRepo ArticleRepo { get;}

        public ICategoryRepo CategoryRepo { get; }
    }
}
