using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketBooking.Migrations
{
    /// <inheritdoc />
    public partial class AddInitActorsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into Actors (name, description, img) values ('Keanu Reeves', 'A', '1.jpg');insert into Actors (name, description, img) values ('Jamie Foxx', 'A', '2.jpg');insert into Actors (name, description, img) values ('Ana de Armas', 'A', '3.jpg');insert into Actors (name, description, img) values ('Leonardo DiCaprio', 'A', '4.jpg');");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from Actors ");
        }

        
    }
}
