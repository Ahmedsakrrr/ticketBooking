using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketBooking.Migrations
{
    /// <inheritdoc />
    public partial class AddInitMovieTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into Movies (name, description, price, statu, dateTime, minImg, categoryId, cinemaId, ActorId) values ('Ballerina', 'Duis mattis egestas metus.', 1.0, 1, '6/14/2025', 'Nunc.txt', 1, 1, 1);insert into Movies (name, description, price, statu, dateTime, minImg, categoryId, cinemaId, ActorId) values ('John Wick', 'Nam tristique tortor eu pede.', 1.0, 0, '8/27/2026', 'VitaeNisl.jpeg', 1, 1, 1);insert into Movies (name, description, price, statu, dateTime, minImg, categoryId, cinemaId, ActorId) values ('Ballerina', 'Nulla facilisi.', 1.0, 0, '7/31/2027', 'MagnaVulputate.avi', 1, 1, 1);insert into Movies (name, description, price, statu, dateTime, minImg, categoryId, cinemaId, ActorId) values ('John Wick', 'In hac habitasse platea dictumst.', 1.0, 1, '6/18/2025', 'Commodo.mp3', 1, 1, 1);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from Movies ");

        }
    }
}
