using Microsoft.EntityFrameworkCore;
using Neureveal.Data.Context;
using Neureveal.Data.Models;
using Neureveal.Data.Repositories.GenericRepo;

namespace Neureveal.Data.Repositories.ArticleRepo
{
    public class ArticleRepo:GenericRepo<Article>,IArticleRepo
    {
        private readonly NewspaperContext context;

        public ArticleRepo(NewspaperContext _context):base(_context)
        {
            context = _context;
        }
        public List<Article> GetAllIncludeCategory()
        {
            return context.Articles.Include(a=>a.Categories).ToList();
        }
        public List<Article> GetArticlesByIds(List<Guid> articlesIds)
        {
            return context.Articles.Where(a => articlesIds.Contains(a.Id)).ToList();
        }
        public Article GetArticleByTitle(string title)
        {
            Article article = context.Articles.Include(a => a.Categories).FirstOrDefault(a => a.Title == title);
            return article;
        }
        public bool ActivateArticle(Guid articleId)
        {
            Article article = context.Articles.FirstOrDefault(a => a.Id== articleId);
            if (article == null)
            {
                return false;
            }
            article.IsActive = true;
            context.SaveChanges();
            return true;
        }
        public bool DeactivateArticle(Guid articleId)
        {
            Article article = context.Articles.FirstOrDefault(a => a.Id == articleId);
            if (article == null)
            {
                return false;
            }
            article.IsActive = false;
            context.SaveChanges();
            return true;
            
        }
        public List<Article> GetActiveArticle()
        {
            return context.Articles.Include(a => a.Categories).Where(a=>a.IsActive==true).ToList();
        }
        public List<Article> GetActiveArticleSorted()
        {
            return context.Articles.Include(a => a.Categories).Where(a => a.IsActive == true).OrderBy(a=>a.CreatedDate).ToList();
        }
    }
}
