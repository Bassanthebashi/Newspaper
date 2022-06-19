namespace Neureveal.Data.Models
{
    public class Article
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }= DateTime.Now;
        public bool IsActive { get; set; } = true;
        
        public virtual ICollection<Category> Categories { get; set; }
        =new HashSet<Category>();
    }
}
