using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TinyClothes.Migrations
{
    public partial class AddAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(maxLength: 60, nullable: false),
                    Username = table.Column<string>(maxLength: 20, nullable: false),
                    Password = table.Column<string>(maxLength: 150, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    Adddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
