namespace Neureveal.Dtos.ArticleDTO
{
    public record ArticleReadDTO
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public string Content { get; init; }
        public DateTime CreatedDate { get; init; }
        public bool IsActive { get; init; }

        public ICollection<CategoryChildReadDTO> Categories { get; init; }
    }
}
