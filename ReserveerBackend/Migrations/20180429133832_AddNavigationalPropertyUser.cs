using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ReserveerBackend.Migrations
{
    public partial class AddNavigationalPropertyUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_GetUserPasswordLogins_PasswordLoginUsername",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PasswordLoginUsername",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GetUserPasswordLogins",
                table: "GetUserPasswordLogins");

            migrationBuilder.DropColumn(
                name: "PasswordLoginUsername",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "GetUserPasswordLogins",
                newName: "UserPasswordLogins");

            migrationBuilder.AddColumn<int>(
                name: "UserdataId",
                table: "UserPasswordLogins",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPasswordLogins",
                table: "UserPasswordLogins",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_UserPasswordLogins_UserdataId",
                table: "UserPasswordLogins",
                column: "UserdataId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPasswordLogins_Users_UserdataId",
                table: "UserPasswordLogins",
                column: "UserdataId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPasswordLogins_Users_UserdataId",
                table: "UserPasswordLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPasswordLogins",
                table: "UserPasswordLogins");

            migrationBuilder.DropIndex(
                name: "IX_UserPasswordLogins_UserdataId",
                table: "UserPasswordLogins");

            migrationBuilder.DropColumn(
                name: "UserdataId",
                table: "UserPasswordLogins");

            migrationBuilder.RenameTable(
                name: "UserPasswordLogins",
                newName: "GetUserPasswordLogins");

            migrationBuilder.AddColumn<string>(
                name: "PasswordLoginUsername",
                table: "Users",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GetUserPasswordLogins",
                table: "GetUserPasswordLogins",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PasswordLoginUsername",
                table: "Users",
                column: "PasswordLoginUsername");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_GetUserPasswordLogins_PasswordLoginUsername",
                table: "Users",
                column: "PasswordLoginUsername",
                principalTable: "GetUserPasswordLogins",
                principalColumn: "Username",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
