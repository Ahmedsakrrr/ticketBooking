using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketBooking.Migrations
{
    /// <inheritdoc />
    public partial class AddInitCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into Categories (name, description, statue) values ('Action', 'A', 1);insert into Categories (name, description, statue) values ('Action', 'A', 0);insert into Categories (name, description, statue) values ('Action', 'A', 0);insert into Categories (name, description, statue) values ('Derama', 'D', 1);");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from Categories ");
        }
    }
}
