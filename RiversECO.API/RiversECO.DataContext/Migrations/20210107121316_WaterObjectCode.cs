using Microsoft.EntityFrameworkCore.Migrations;

namespace RiversECO.DataContext.Migrations
{
    public partial class WaterObjectCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "WaterObjects");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "WaterObjects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeSwb",
                table: "WaterObjects",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "WaterObjects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeCode",
                table: "WaterObjects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeName",
                table: "WaterObjects",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WaterObjects_CodeSwb",
                table: "WaterObjects",
                column: "CodeSwb",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WaterObjects_CodeSwb",
                table: "WaterObjects");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "WaterObjects");

            migrationBuilder.DropColumn(
                name: "CodeSwb",
                table: "WaterObjects");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "WaterObjects");

            migrationBuilder.DropColumn(
                name: "TypeCode",
                table: "WaterObjects");

            migrationBuilder.DropColumn(
                name: "TypeName",
                table: "WaterObjects");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "WaterObjects",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
