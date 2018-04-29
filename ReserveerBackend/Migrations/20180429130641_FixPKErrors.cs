using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ReserveerBackend.Migrations
{
    public partial class FixPKErrors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_Reservations_ReservationId",
                table: "Report");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_Rooms_RoomId",
                table: "Report");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_Users_UserId",
                table: "Report");

            migrationBuilder.DropForeignKey(
                name: "FK_Temperature_Rooms_RoomId",
                table: "Temperature");

            migrationBuilder.DropForeignKey(
                name: "FK_Temperature_TemperatureSensor_SensorId",
                table: "Temperature");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserPasswordLogin_PasswordLoginUsername",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Whiteboard_Rooms_RoomId",
                table: "Whiteboard");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Whiteboard",
                table: "Whiteboard");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPasswordLogin",
                table: "UserPasswordLogin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TemperatureSensor",
                table: "TemperatureSensor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Temperature",
                table: "Temperature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Report",
                table: "Report");

            migrationBuilder.RenameTable(
                name: "Whiteboard",
                newName: "Whiteboards");

            migrationBuilder.RenameTable(
                name: "UserPasswordLogin",
                newName: "GetUserPasswordLogins");

            migrationBuilder.RenameTable(
                name: "TemperatureSensor",
                newName: "TemperatureSensors");

            migrationBuilder.RenameTable(
                name: "Temperature",
                newName: "Temperatures");

            migrationBuilder.RenameTable(
                name: "Report",
                newName: "Reports");

            migrationBuilder.RenameIndex(
                name: "IX_Whiteboard_RoomId",
                table: "Whiteboards",
                newName: "IX_Whiteboards_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Temperature_SensorId",
                table: "Temperatures",
                newName: "IX_Temperatures_SensorId");

            migrationBuilder.RenameIndex(
                name: "IX_Temperature_RoomId",
                table: "Temperatures",
                newName: "IX_Temperatures_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Report_UserId",
                table: "Reports",
                newName: "IX_Reports_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Report_RoomId",
                table: "Reports",
                newName: "IX_Reports_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Report_ReservationId",
                table: "Reports",
                newName: "IX_Reports_ReservationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Whiteboards",
                table: "Whiteboards",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GetUserPasswordLogins",
                table: "GetUserPasswordLogins",
                column: "Username");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TemperatureSensors",
                table: "TemperatureSensors",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Temperatures",
                table: "Temperatures",
                column: "DateTime");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reports",
                table: "Reports",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ParticipantChanges",
                columns: table => new
                {
                    shadowidgen1
                    = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ChangeDate = table.Column<DateTime>(nullable: false),
                    OldIsOwner = table.Column<bool>(nullable: false),
                    ReservationId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantChanges", x => x.shadowidgen1);
                    table.ForeignKey(
                        name: "FK_ParticipantChanges_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParticipantChanges_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    shadowid = table.Column<string>(nullable: false),
                    IsOwner = table.Column<bool>(nullable: false),
                    ReservationId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.shadowid);
                    table.ForeignKey(
                        name: "FK_Participants_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Participants_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservationChanges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ChangeDate = table.Column<DateTime>(nullable: false),
                    OldActive = table.Column<bool>(nullable: false),
                    OldEndDate = table.Column<DateTime>(nullable: false),
                    OldStatDate = table.Column<DateTime>(nullable: false),
                    ReservationId = table.Column<int>(nullable: false),
                    RoomId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationChanges_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationChanges_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationChanges_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantChanges_ReservationId",
                table: "ParticipantChanges",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantChanges_UserId",
                table: "ParticipantChanges",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_ReservationId",
                table: "Participants",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_UserId",
                table: "Participants",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationChanges_ReservationId",
                table: "ReservationChanges",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationChanges_RoomId",
                table: "ReservationChanges",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationChanges_UserId",
                table: "ReservationChanges",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Reservations_ReservationId",
                table: "Reports",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Rooms_RoomId",
                table: "Reports",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Users_UserId",
                table: "Reports",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Temperatures_Rooms_RoomId",
                table: "Temperatures",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Temperatures_TemperatureSensors_SensorId",
                table: "Temperatures",
                column: "SensorId",
                principalTable: "TemperatureSensors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_GetUserPasswordLogins_PasswordLoginUsername",
                table: "Users",
                column: "PasswordLoginUsername",
                principalTable: "GetUserPasswordLogins",
                principalColumn: "Username",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Whiteboards_Rooms_RoomId",
                table: "Whiteboards",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Reservations_ReservationId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Rooms_RoomId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Users_UserId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Temperatures_Rooms_RoomId",
                table: "Temperatures");

            migrationBuilder.DropForeignKey(
                name: "FK_Temperatures_TemperatureSensors_SensorId",
                table: "Temperatures");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_GetUserPasswordLogins_PasswordLoginUsername",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Whiteboards_Rooms_RoomId",
                table: "Whiteboards");

            migrationBuilder.DropTable(
                name: "ParticipantChanges");

            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropTable(
                name: "ReservationChanges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Whiteboards",
                table: "Whiteboards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TemperatureSensors",
                table: "TemperatureSensors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Temperatures",
                table: "Temperatures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reports",
                table: "Reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GetUserPasswordLogins",
                table: "GetUserPasswordLogins");

            migrationBuilder.RenameTable(
                name: "Whiteboards",
                newName: "Whiteboard");

            migrationBuilder.RenameTable(
                name: "TemperatureSensors",
                newName: "TemperatureSensor");

            migrationBuilder.RenameTable(
                name: "Temperatures",
                newName: "Temperature");

            migrationBuilder.RenameTable(
                name: "Reports",
                newName: "Report");

            migrationBuilder.RenameTable(
                name: "GetUserPasswordLogins",
                newName: "UserPasswordLogin");

            migrationBuilder.RenameIndex(
                name: "IX_Whiteboards_RoomId",
                table: "Whiteboard",
                newName: "IX_Whiteboard_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Temperatures_SensorId",
                table: "Temperature",
                newName: "IX_Temperature_SensorId");

            migrationBuilder.RenameIndex(
                name: "IX_Temperatures_RoomId",
                table: "Temperature",
                newName: "IX_Temperature_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_UserId",
                table: "Report",
                newName: "IX_Report_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_RoomId",
                table: "Report",
                newName: "IX_Report_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_ReservationId",
                table: "Report",
                newName: "IX_Report_ReservationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Whiteboard",
                table: "Whiteboard",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TemperatureSensor",
                table: "TemperatureSensor",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Temperature",
                table: "Temperature",
                column: "DateTime");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Report",
                table: "Report",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPasswordLogin",
                table: "UserPasswordLogin",
                column: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_Reservations_ReservationId",
                table: "Report",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_Rooms_RoomId",
                table: "Report",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_Users_UserId",
                table: "Report",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Temperature_Rooms_RoomId",
                table: "Temperature",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Temperature_TemperatureSensor_SensorId",
                table: "Temperature",
                column: "SensorId",
                principalTable: "TemperatureSensor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserPasswordLogin_PasswordLoginUsername",
                table: "Users",
                column: "PasswordLoginUsername",
                principalTable: "UserPasswordLogin",
                principalColumn: "Username",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Whiteboard_Rooms_RoomId",
                table: "Whiteboard",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
