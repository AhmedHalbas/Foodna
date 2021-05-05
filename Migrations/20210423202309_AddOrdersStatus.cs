using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantProject.Migrations
{
    public partial class AddOrdersStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
               name: "orderStatus",
               table: "Orders",
               type: "int",
               nullable: false,
               defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
               name: "orderStatus",
               table: "Orders");
        }
    }
}
