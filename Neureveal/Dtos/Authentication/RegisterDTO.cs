namespace Neureveal.Dtos.Authentication
{
    public record RegisterDTO
    {
        public string UserName { get; init; }

        public string Email { get; init; }
        public string Password { get; init; }
    }
}
