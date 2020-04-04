using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductShop.Data.Migrations
{
    public partial class IndexForSubactegories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Subcategories",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Subcategories_Name",
                table: "Subcategories",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Subcategories_Name",
                table: "Subcategories");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Subcategories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
