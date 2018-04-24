using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ReserveerBackend.Migrations
{
    public partial class FixedRoomStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Room_RoomId",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_RoomId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "TemperatureDateTime",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Item");

            migrationBuilder.RenameColumn(
                name: "Temperature",
                table: "Room",
                newName: "Powersupply");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Room",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Smartboard",
                table: "Room",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TV",
                table: "Room",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "Smartboard",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "TV",
                table: "Room");

            migrationBuilder.RenameColumn(
                name: "Powersupply",
                table: "Room",
                newName: "Temperature");

            migrationBuilder.AddColumn<DateTime>(
                name: "TemperatureDateTime",
                table: "Room",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Item",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Item_RoomId",
                table: "Item",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Room_RoomId",
                table: "Item",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
