using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RiversECO.DataContext.Migrations
{
    public partial class InfluenceParameter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Criterias",
                table: "Reviews");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "WaterObjects",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Reviews",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "CriteriaId",
                table: "Reviews",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "GlobalInfluence",
                table: "Reviews",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Influence",
                table: "Reviews",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "References",
                table: "Reviews",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CriteriaId",
                table: "Reviews",
                column: "CriteriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Criterias_CriteriaId",
                table: "Reviews",
                column: "CriteriaId",
                principalTable: "Criterias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Criterias_CriteriaId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_CriteriaId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "WaterObjects");

            migrationBuilder.DropColumn(
                name: "CriteriaId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "GlobalInfluence",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Influence",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "References",
                table: "Reviews");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Criterias",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
