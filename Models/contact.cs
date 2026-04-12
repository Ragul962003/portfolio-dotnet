namespace PortfolioApi.Models
{
    public class contact
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Message { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}