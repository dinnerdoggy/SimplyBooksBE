using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimplyBooksBE.Migrations
{
    /// <inheritdoc />
    public partial class TweakedBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Price" },
                values: new object[] { "It's just an okay book", 99.99m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Price" },
                values: new object[] { "The Best Book Ever", 49.99m });
        }
    }
}
