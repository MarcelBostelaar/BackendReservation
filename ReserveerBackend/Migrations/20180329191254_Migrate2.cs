using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ReserveerBackend.Migrations
{
    public partial class Migrate2 : Migration
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
                name: "RoomId",
                table: "Item");

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "Room",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Room_ItemId",
                table: "Room",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Item_ItemId",
                table: "Room",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Item_ItemId",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Room_ItemId",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Room");

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
