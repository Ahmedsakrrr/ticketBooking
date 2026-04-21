using Microsoft.EntityFrameworkCore;

namespace TicketBooking.Models
{
    [PrimaryKey(nameof(CinemaId), nameof(SubImg))]
    public class MovieSubImg
    {
        public Cinema Cinema { get; set; }
        public int CinemaId { get; set; }
        public string SubImg { get; set; }= "defaultImage.png";
    }
}
