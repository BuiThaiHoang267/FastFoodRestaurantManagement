using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastFoodManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseV03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "OrderItems",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "OrderItems");
        }
    }
}
