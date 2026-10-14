using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedTools.Identity.API.Migrations
{
    /// <inheritdoc />
    public partial class AddPermissionDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "permissions",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "permissions");
        }
    }
}
