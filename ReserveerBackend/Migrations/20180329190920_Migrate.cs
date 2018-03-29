using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ReserveerBackend.Migrations
{
    public partial class Migrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Item_ItemsId",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Room_ItemsId",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "ItemsId",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "ItemsId",
                table: "Room",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Room_ItemsId",
                table: "Room",
                column: "ItemsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Item_ItemsId",
                table: "Room",
                column: "ItemsId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
