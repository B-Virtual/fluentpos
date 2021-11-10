using Microsoft.EntityFrameworkCore.Migrations;

namespace FluentPOS.Modules.Sales.Infrastructure.Persistence.Migrations
{
    public partial class ProductName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "Sales",
                table: "Products",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "Sales",
                table: "Products");
        }
    }
}
