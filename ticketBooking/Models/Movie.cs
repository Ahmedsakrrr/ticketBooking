namespace TicketBooking.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool Statu { get; set; }
        public DateTime DateTime { get; set; }
        public string MinImg { get; set; }= "defaultImage.png";
        public List<Actor> Actors { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public Cinema Cinema { get; set; }
        public int CinemaId { get; set; }


    }
}
