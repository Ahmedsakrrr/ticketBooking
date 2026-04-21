

namespace TicketBooking.Data
{
    public class TicketBookingDbContext: DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseSqlServer("Server=.;Database=TicketBookingDb;Trusted_Connection=True;TrustServerCertificate=True");
        }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieSubImg> MovieSubImgs { get; set; }

    }
}
