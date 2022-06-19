namespace Neureveal.Dtos.Token
{
    public record TokenDTO
    {
        public string Token { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}
