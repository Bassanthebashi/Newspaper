namespace Neureveal.Dtos.ArticleDTO
{
    public record ArticleWriteDTO
    {
        public Guid Id { get; set; }
        public string Title { get; init; }
        public string Content { get; init; }
        public List<Guid> CategoryIds { get; init; }
    }
}
