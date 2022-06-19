using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Neureveal.Data.Models;
using Neureveal.Data.Repositories.UnitOfWork;
using Neureveal.Dtos.CategoryDTO;

namespace Neureveal.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CategoryController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        //Lazy Loading
        [HttpGet]
        public ActionResult<List<Category>> GetAll()
        {
            return unitOfWork.CategoryRepo.GetAll();
        }
        //Eager Loading
        [HttpGet]
        [Route("IncludingArticles")]
        public ActionResult<List<CategoryReadDTO>> GetAllIncludingArticles()
        {
            
            var categoriesFromDB =unitOfWork.CategoryRepo.GetAllIncludeArticles();
            return mapper.Map<List<CategoryReadDTO>>(categoriesFromDB);
        }
        [HttpGet("{categoryId:Guid}")]
        public ActionResult<Category> GetById(Guid categoryId)
        {
            var category =unitOfWork.CategoryRepo.GetById(categoryId);
            if (category == null) return BadRequest("Category Not Found");
            return category;
        }
        [HttpPost]
        public ActionResult Add(CategoryWriteDTO categoryWriteDTO)
        {
            
            Category categoryToAdd=mapper.Map<Category>(categoryWriteDTO);

            categoryToAdd.Id = Guid.NewGuid();
            var articlesList = unitOfWork.ArticleRepo.GetArticlesByIds(categoryWriteDTO.ArticleIds);
            categoryToAdd.Articles = articlesList;

            unitOfWork.CategoryRepo.Add(categoryToAdd);
            unitOfWork.CategoryRepo.SaveChanges();
            var categoryToReturn = mapper.Map<CategoryReadDTO>(categoryToAdd);

            return CreatedAtAction("GetById", new { categoryId = categoryToAdd.Id }, categoryToReturn);
        }
        
        [HttpPut("{categoryId:Guid}")]
        public ActionResult Edit(Guid categoryId,CategoryWriteDTO categoryWriteDTO)
        {
            if (categoryId != categoryWriteDTO.Id)
            {
                return BadRequest("Wrong ID");
            }
            Category category = unitOfWork.CategoryRepo.GetById(categoryId);
            if (category == null)
            {
                return NotFound("Category isn't exist");
            }

            var categoryToEdit = mapper.Map(categoryWriteDTO,category);

            unitOfWork.CategoryRepo.Update(categoryToEdit);
            unitOfWork.CategoryRepo.SaveChanges();

            return Ok("updated");

        }
        
        [HttpDelete("{categoryId:Guid}")]
        public ActionResult Delete(Guid categoryId)
        {
            unitOfWork.CategoryRepo.Delete(categoryId);
            unitOfWork.CategoryRepo.SaveChanges();
            return Ok("Deleted");
        }
    }
}
