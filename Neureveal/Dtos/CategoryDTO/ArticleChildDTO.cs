namespace Neureveal.Dtos.CategoryDTO
{
    public record ArticleChildDTO
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public string Content { get; init; }
    }
}
