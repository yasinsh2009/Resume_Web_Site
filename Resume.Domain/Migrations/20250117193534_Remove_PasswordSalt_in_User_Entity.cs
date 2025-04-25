using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Resume.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Remove_PasswordSalt_in_User_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaswordSalt",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaswordSalt",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
