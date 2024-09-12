using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DragonAcc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class donedboke : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "InGameItems");

            migrationBuilder.RenameColumn(
                name: "Reward",
                table: "DailyCheckIns",
                newName: "Coin");

            migrationBuilder.AddColumn<string>(
                name: "GameAccountId",
                table: "InGameItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StarQ",
                table: "InGameItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "GameServices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountPasswordChanged",
                table: "GameAccounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Auctions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionId = table.Column<int>(type: "int", nullable: true),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Seri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dentistry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionDetails", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "TransactionDetails");

            migrationBuilder.DropColumn(
                name: "GameAccountId",
                table: "InGameItems");

            migrationBuilder.DropColumn(
                name: "StarQ",
                table: "InGameItems");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GameServices");

            migrationBuilder.DropColumn(
                name: "AccountPasswordChanged",
                table: "GameAccounts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Auctions");

            migrationBuilder.RenameColumn(
                name: "Coin",
                table: "DailyCheckIns",
                newName: "Reward");

            migrationBuilder.AddColumn<string>(
                name: "Amount",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "InGameItems",
                type: "int",
                nullable: true);
        }
    }
}
