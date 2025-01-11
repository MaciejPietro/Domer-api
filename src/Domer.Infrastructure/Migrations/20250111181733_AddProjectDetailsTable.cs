using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectDetailsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuildingArea",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "UsableArea",
                table: "Projects");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Projects",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProjectDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsableArea = table.Column<int>(type: "integer", nullable: false),
                    BuildingArea = table.Column<int>(type: "integer", nullable: false),
                    Urls = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectDetails_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectImage_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDetails_ProjectId",
                table: "ProjectDetails",
                column: "ProjectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectImage_ProjectId",
                table: "ProjectImage",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectDetails");

            migrationBuilder.DropTable(
                name: "ProjectImage");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Projects");

            migrationBuilder.AddColumn<int>(
                name: "BuildingArea",
                table: "Projects",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsableArea",
                table: "Projects",
                type: "integer",
                nullable: true);
        }
    }
}
