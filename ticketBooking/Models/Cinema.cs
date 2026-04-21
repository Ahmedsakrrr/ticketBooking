namespace TicketBooking.Models
{
    public class Cinema
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string Img { get; set; } = "DefaultImage.png";
        public decimal Price { get; set; }
    }
}
