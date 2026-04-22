using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kompass.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDeviceToProjectDevice2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DeviceId1",
                table: "ProjectDevice",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDevice_DeviceId1",
                table: "ProjectDevice",
                column: "DeviceId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDevice_Devices_DeviceId1",
                table: "ProjectDevice",
                column: "DeviceId1",
                principalTable: "Devices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDevice_Devices_DeviceId1",
                table: "ProjectDevice");

            migrationBuilder.DropIndex(
                name: "IX_ProjectDevice_DeviceId1",
                table: "ProjectDevice");

            migrationBuilder.DropColumn(
                name: "DeviceId1",
                table: "ProjectDevice");
        }
    }
}
