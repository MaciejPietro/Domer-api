using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProjectDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "BuildingArea",
                table: "ProjectDetails");

            migrationBuilder.DropColumn(
                name: "UsableArea",
                table: "ProjectDetails");

            migrationBuilder.AddColumn<int>(
                name: "AdvertType",
                table: "ProjectDetails",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AdvertiserType",
                table: "ProjectDetails",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ConditionType",
                table: "ProjectDetails",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MarketType",
                table: "ProjectDetails",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OwnershipType",
                table: "ProjectDetails",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "ProjectDetails",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdvertType",
                table: "ProjectDetails");

            migrationBuilder.DropColumn(
                name: "AdvertiserType",
                table: "ProjectDetails");

            migrationBuilder.DropColumn(
                name: "ConditionType",
                table: "ProjectDetails");

            migrationBuilder.DropColumn(
                name: "MarketType",
                table: "ProjectDetails");

            migrationBuilder.DropColumn(
                name: "OwnershipType",
                table: "ProjectDetails");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "ProjectDetails");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Projects",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BuildingArea",
                table: "ProjectDetails",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsableArea",
                table: "ProjectDetails",
                type: "integer",
                nullable: true);
        }
    }
}
