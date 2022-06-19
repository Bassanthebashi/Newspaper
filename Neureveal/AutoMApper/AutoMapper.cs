using AutoMapper;
using Neureveal.Data.Models;
using Neureveal.Dtos.ArticleDTO;
using Neureveal.Dtos.CategoryDTO;

namespace Neureveal.AutoMApper
{
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<Category, CategoryChildReadDTO>();
            CreateMap<Article, ArticleReadDTO>();
            CreateMap<ArticleWriteDTO, Article>();

            CreateMap<Article, ArticleChildDTO>();
            CreateMap<Category, CategoryReadDTO>();
            CreateMap<CategoryWriteDTO, Category>();

        }
    }
}
