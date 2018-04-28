using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ReserveerBackend.Migrations
{
    public partial class SeperatedPasswordLogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "PasswordLoginUsername",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserPasswordLogin",
                columns: table => new
                {
                    Username = table.Column<string>(nullable: false),
                    HashedPassword = table.Column<byte[]>(nullable: false),
                    Salt = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPasswordLogin", x => x.Username);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_PasswordLoginUsername",
                table: "Users",
                column: "PasswordLoginUsername");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserPasswordLogin_PasswordLoginUsername",
                table: "Users",
                column: "PasswordLoginUsername",
                principalTable: "UserPasswordLogin",
                principalColumn: "Username",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserPasswordLogin_PasswordLoginUsername",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserPasswordLogin");

            migrationBuilder.DropIndex(
                name: "IX_Users_PasswordLoginUsername",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordLoginUsername",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                nullable: false,
                defaultValue: "");
        }
    }
}
