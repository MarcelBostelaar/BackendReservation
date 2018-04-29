using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ReserveerBackend.Migrations
{
    public partial class testmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Reservations_ReservationId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Users_UserId",
                table: "Participants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Participants",
                table: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_Participants_UserId",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "shadowid",
                table: "Participants");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Participants",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "ReservationId",
                table: "Participants",
                newName: "ReservationID");

            migrationBuilder.RenameIndex(
                name: "IX_Participants_ReservationId",
                table: "Participants",
                newName: "IX_Participants_ReservationID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Participants",
                table: "Participants",
                columns: new[] { "UserID", "ReservationID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Reservations_ReservationID",
                table: "Participants",
                column: "ReservationID",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Users_UserID",
                table: "Participants",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Reservations_ReservationID",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Users_UserID",
                table: "Participants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Participants",
                table: "Participants");

            migrationBuilder.RenameColumn(
                name: "ReservationID",
                table: "Participants",
                newName: "ReservationId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Participants",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Participants_ReservationID",
                table: "Participants",
                newName: "IX_Participants_ReservationId");

            migrationBuilder.AddColumn<string>(
                name: "shadowid",
                table: "Participants",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Participants",
                table: "Participants",
                column: "shadowid");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_UserId",
                table: "Participants",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Reservations_ReservationId",
                table: "Participants",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Users_UserId",
                table: "Participants",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
