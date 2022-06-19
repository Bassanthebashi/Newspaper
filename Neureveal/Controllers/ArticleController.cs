using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Neureveal.Data.Models;
using Neureveal.Data.Repositories.UnitOfWork;
using Neureveal.Dtos.ArticleDTO;

namespace Neureveal.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public IMapper Mapper { get; }

        public ArticleController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            Mapper = mapper;
        }
        
        //Lazy Loading
        [HttpGet]
        public ActionResult<List<Article>> GetAll()
        {
            return unitOfWork.ArticleRepo.GetAll();
        }

        //Eager Loading
        [HttpGet]
        [Route("IncludingCategories")]
        public ActionResult<List<ArticleReadDTO>> GetAllWithCategories()
        {
            List<Article> DBArticles= unitOfWork.ArticleRepo.GetAllIncludeCategory();
            
            return Mapper.Map<List<ArticleReadDTO>>(DBArticles);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Active")]
        public ActionResult<List<ArticleReadDTO>> GetActiveArticle()
        {
            List<Article> DBArticles = unitOfWork.ArticleRepo.GetActiveArticle();
            return Mapper.Map<List<ArticleReadDTO>>(DBArticles);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ActiveSorted")]
        public ActionResult<List<ArticleReadDTO>> GetActiveArticleSorted()
        {
            List<Article> DBArticles =unitOfWork.ArticleRepo.GetActiveArticleSorted();
            return Mapper.Map<List<ArticleReadDTO>>(DBArticles);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ByTitle")]
        public ActionResult<ArticleReadDTO> GetArticleByTitle(string title)
        {
            Article DBArticle = unitOfWork.ArticleRepo.GetArticleByTitle(title);
            return  Mapper.Map<ArticleReadDTO>(DBArticle);
        }

        [HttpGet("{articleId:Guid}")]
        public ActionResult<Article> GetById(Guid articleId)
        {
            var article = unitOfWork.ArticleRepo.GetById(articleId);
            if (article == null) return BadRequest("Article Not Found");
            return article;
        }

        [HttpPost]
        public ActionResult Add(ArticleWriteDTO articleWriteDTO)
        {
            var articleToAdd = Mapper.Map<Article>(articleWriteDTO);

            var articleCategoryList = unitOfWork.CategoryRepo.GetCategoriesByIds(articleWriteDTO.CategoryIds);
            articleToAdd.Id=Guid.NewGuid();
            articleToAdd.Categories= articleCategoryList;
            
            unitOfWork.ArticleRepo.Add(articleToAdd);
            unitOfWork.ArticleRepo.SaveChanges();
            var articleToreturn = Mapper.Map<ArticleReadDTO>(articleToAdd);
            return CreatedAtAction("GetById", new { articleId = articleToAdd.Id }, articleToreturn);

        }
        

        [HttpPut("{articleId:Guid}")]
        public ActionResult Edit(Guid articleId,ArticleWriteDTO articleWriteDTO)
        {
            if (articleId != articleWriteDTO.Id)
            {
                return BadRequest("Wrong ID");
            }
            var article = unitOfWork.ArticleRepo.GetById(articleId);
            if (article == null)
            {
                return NotFound("No article with this Id");
            }

            var articleToEdit = Mapper.Map(articleWriteDTO, article);
            
            unitOfWork.ArticleRepo.Update(articleToEdit);
            unitOfWork.ArticleRepo.SaveChanges();
            return Ok("updated");

        }



        [HttpDelete("{articleId}")]
        public ActionResult Delete(Guid articleId)
        {
            unitOfWork.ArticleRepo.Delete(articleId);
            unitOfWork.ArticleRepo.SaveChanges();
            return Ok("Deleted");
        }



        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Activate/{articleId:Guid}")]
        public ActionResult Activate(Guid articleId)
        {
            bool res=unitOfWork.ArticleRepo.ActivateArticle(articleId);
            if (!res)
            {
                return BadRequest();
            }
            return Ok("Activated");
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Deactivate/{articleId:Guid}")]
        public ActionResult Deactivate(Guid articleId)
        {
            bool res=unitOfWork.ArticleRepo.DeactivateArticle(articleId);
            if (!res)
            {
                return BadRequest();
            }
            return Ok("Deactivated");
        }
        

    }
}
