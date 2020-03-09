using Microsoft.EntityFrameworkCore.Migrations;

namespace PRSCapstone.Migrations
{
    public partial class changedrequestpricetototal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Requests",
                newName: "Total");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Total",
                table: "Requests",
                newName: "Price");
        }
    }
}
