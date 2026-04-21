namespace TicketBooking.ViewModels
{
    public class MovieVM
    {
        public string Name { get; set; }
        
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int CinemaId { get; set; }
        public int ActorId { get; set; }
        
        public int Page { get; set; } = 1;
    }
}
