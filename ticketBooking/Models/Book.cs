using System.ComponentModel.DataAnnotations;

namespace TicketBooking.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }= string.Empty;
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime MovieDate { get; set; }

        [Required]
        public string Movieime { get; set; }
    }
}
