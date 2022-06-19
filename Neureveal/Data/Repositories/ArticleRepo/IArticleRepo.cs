using Neureveal.Data.Models;
using Neureveal.Data.Repositories.GenericRepo;

namespace Neureveal.Data.Repositories.ArticleRepo
{
    public interface IArticleRepo:IGenericRepo<Article>
    {
        public List<Article> GetAllIncludeCategory();
        public List<Article> GetArticlesByIds(List<Guid> articlesIds);
        public Article GetArticleByTitle(string title);
        public bool ActivateArticle(Guid articleId);
        public bool DeactivateArticle(Guid articleId);

        public List<Article> GetActiveArticle();
        public List<Article> GetActiveArticleSorted();

    }
}
