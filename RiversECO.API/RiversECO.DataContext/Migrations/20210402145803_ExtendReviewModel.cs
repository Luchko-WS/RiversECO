using Microsoft.EntityFrameworkCore.Migrations;

namespace RiversECO.DataContext.Migrations
{
    public partial class ExtendReviewModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GlobalInfluence",
                table: "Reviews");

            migrationBuilder.AlterColumn<int>(
                name: "Influence",
                table: "Reviews",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Certainty",
                table: "Reviews",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAnonymous",
                table: "Reviews",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ReferenceType",
                table: "Reviews",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Certainty",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "IsAnonymous",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ReferenceType",
                table: "Reviews");

            migrationBuilder.AlterColumn<int>(
                name: "Influence",
                table: "Reviews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "GlobalInfluence",
                table: "Reviews",
                type: "int",
                nullable: true);
        }
    }
}
