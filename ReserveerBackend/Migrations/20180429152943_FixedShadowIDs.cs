using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ReserveerBackend.Migrations
{
    public partial class FixedShadowIDs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantChanges_Reservations_ReservationId",
                table: "ParticipantChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantChanges_Users_UserId",
                table: "ParticipantChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPasswordLogins_Users_UserdataId",
                table: "UserPasswordLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParticipantChanges",
                table: "ParticipantChanges");

            migrationBuilder.DropIndex(
                name: "IX_ParticipantChanges_UserId",
                table: "ParticipantChanges");

            migrationBuilder.DropColumn(
                name: "shadowid",
                table: "ParticipantChanges");

            migrationBuilder.RenameColumn(
                name: "UserdataId",
                table: "UserPasswordLogins",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPasswordLogins_UserdataId",
                table: "UserPasswordLogins",
                newName: "IX_UserPasswordLogins_UserId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ParticipantChanges",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "ReservationId",
                table: "ParticipantChanges",
                newName: "ReservationID");

            migrationBuilder.RenameIndex(
                name: "IX_ParticipantChanges_ReservationId",
                table: "ParticipantChanges",
                newName: "IX_ParticipantChanges_ReservationID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParticipantChanges",
                table: "ParticipantChanges",
                columns: new[] { "UserID", "ReservationID", "ChangeDate" });

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantChanges_Reservations_ReservationID",
                table: "ParticipantChanges",
                column: "ReservationID",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantChanges_Users_UserID",
                table: "ParticipantChanges",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPasswordLogins_Users_UserId",
                table: "UserPasswordLogins",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantChanges_Reservations_ReservationID",
                table: "ParticipantChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantChanges_Users_UserID",
                table: "ParticipantChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPasswordLogins_Users_UserId",
                table: "UserPasswordLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParticipantChanges",
                table: "ParticipantChanges");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserPasswordLogins",
                newName: "UserdataId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPasswordLogins_UserId",
                table: "UserPasswordLogins",
                newName: "IX_UserPasswordLogins_UserdataId");

            migrationBuilder.RenameColumn(
                name: "ReservationID",
                table: "ParticipantChanges",
                newName: "ReservationId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "ParticipantChanges",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ParticipantChanges_ReservationID",
                table: "ParticipantChanges",
                newName: "IX_ParticipantChanges_ReservationId");

            migrationBuilder.AddColumn<int>(
                name: "shadowid",
                table: "ParticipantChanges",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParticipantChanges",
                table: "ParticipantChanges",
                column: "shadowid");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantChanges_UserId",
                table: "ParticipantChanges",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantChanges_Reservations_ReservationId",
                table: "ParticipantChanges",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantChanges_Users_UserId",
                table: "ParticipantChanges",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPasswordLogins_Users_UserdataId",
                table: "UserPasswordLogins",
                column: "UserdataId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
