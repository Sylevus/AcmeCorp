using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcmeCorps.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    EmailAddress = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "RolesMaster",
                columns: table => new
                {
                    RoleId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleName = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesMaster", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "UsersMaster",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersMaster", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Tax = table.Column<decimal>(type: "TEXT", nullable: false),
                    Total = table.Column<decimal>(type: "TEXT", nullable: false),
                    PaymentTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    PaymentAccountNumber = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    RefreshTokenId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Token = table.Column<string>(type: "TEXT", nullable: false),
                    JwtId = table.Column<string>(type: "TEXT", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Used = table.Column<bool>(type: "INTEGER", nullable: true),
                    UserId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.RefreshTokenId);
                    table.ForeignKey(
                        name: "FK_RefreshToken_UsersMaster_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersMaster",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserRolesId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<long>(type: "INTEGER", nullable: false),
                    RoleId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.UserRolesId);
                    table.ForeignKey(
                        name: "FK_UserRoles_RolesMaster_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RolesMaster",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_UsersMaster_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersMaster",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "CustomerId", "EmailAddress", "FirstName", "LastName" },
                values: new object[] { 1, "Customer1@dev.com", "Darth", "Vader" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "CustomerId", "EmailAddress", "FirstName", "LastName" },
                values: new object[] { 2, "Customer2@dev.com", "ObiWan", "Kenobi" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "CustomerId", "EmailAddress", "FirstName", "LastName" },
                values: new object[] { 3, "Customer3@dev.com", "Luke", "Skywalker" });

            migrationBuilder.InsertData(
                table: "RolesMaster",
                columns: new[] { "RoleId", "CreatedOn", "RoleName", "UpdatedOn" },
                values: new object[] { 1L, new DateTime(2023, 2, 21, 18, 17, 10, 884, DateTimeKind.Local).AddTicks(7692), "Admin", new DateTime(2023, 2, 21, 18, 17, 10, 884, DateTimeKind.Local).AddTicks(7726) });

            migrationBuilder.InsertData(
                table: "UsersMaster",
                columns: new[] { "UserId", "CreatedOn", "Email", "FirstName", "LastName", "Password", "PhoneNumber", "UpdatedOn", "UserName" },
                values: new object[] { 1L, new DateTime(2023, 2, 21, 18, 17, 10, 884, DateTimeKind.Local).AddTicks(7789), "taylor.walston@comcast.net", "Taylor", "Walston", "Password", "5555555555", new DateTime(2023, 2, 21, 18, 17, 10, 884, DateTimeKind.Local).AddTicks(7792), "twalston" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserRolesId", "RoleId", "UserId" },
                values: new object[] { 1L, 1L, 1L });

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "RolesMaster");

            migrationBuilder.DropTable(
                name: "UsersMaster");
        }
    }
}
