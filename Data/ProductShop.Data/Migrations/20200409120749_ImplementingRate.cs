using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductShop.Data.Migrations
{
    public partial class ImplementingRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Ratings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Ratings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Ratings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_IsDeleted",
                table: "Ratings",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ratings_IsDeleted",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Ratings");
        }
    }
}
