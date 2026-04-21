using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketBooking.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumnInMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Kind",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kind",
                table: "Movies");
        }
    }
}
