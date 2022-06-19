namespace Neureveal.Dtos.CategoryDTO
{
    public record CategoryWriteDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public List<Guid> ArticleIds { get; init; }
    }
}
