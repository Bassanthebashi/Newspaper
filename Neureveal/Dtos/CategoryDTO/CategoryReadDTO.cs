namespace Neureveal.Dtos.CategoryDTO
{
    public class CategoryReadDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public ICollection<ArticleChildDTO> Articles { get; init; }
    }
}
