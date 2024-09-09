using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DragonAcc.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedb1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GameAccounts");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "GameAccounts",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "GameAccounts");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "GameAccounts",
                type: "integer",
                nullable: true);
        }
    }
}
