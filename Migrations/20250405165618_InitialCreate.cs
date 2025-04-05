using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SimplyBooksBE.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    Favorite = table.Column<bool>(type: "boolean", nullable: false),
                    Uid = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AuthorId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Sale = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Uid = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Email", "Favorite", "FirstName", "Image", "LastName", "Uid" },
                values: new object[,]
                {
                    { 1, "dinnerdoggy@gmail.com", true, "Casey", "https://avatars.githubusercontent.com/u/31261276?v=4", "Cunningham", "oQWpgCUQhWfVTf3fVK6G5XdG7Z73" },
                    { 2, "email@email.com", false, "John", "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5a/John_Doe%2C_born_John_Nommensen_Duchac.jpg/1200px-John_Doe%2C_born_John_Nommensen_Duchac.jpg", "Doe", "oQWpgCUQhWfVTf3fVK6G5XdG7Z73" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Description", "Image", "Price", "Sale", "Title", "Uid" },
                values: new object[,]
                {
                    { 1, 1, "The Best Book Ever", "https://media.canva.com/v2/files/uri:ifs%3A%2F%2FM%2F9e5ef14c-b183-4307-a6e1-c0fdf559d5b6?csig=AAAAAAAAAAAAAAAAAAAAABYvq5u88tkGEvkQadcue_6AdY_e7KxdVct1etJC4glY&exp=1743750464&signer=media-rpc&token=AAIAAU0AJDllNWVmMTRjLWIxODMtNDMwNy1hNmUxLWMwZmRmNTU5ZDViNgAAAAABlf-gggDo8EPWDxB_7R8vCdmFIhCgucvH9pcq6GuRg0Kh6Eohvg", 49.99m, false, "Casey's Best Book", "oQWpgCUQhWfVTf3fVK6G5XdG7Z73" },
                    { 2, 2, "The Best Book Ever", "https://media.canva.com/v2/files/uri:ifs%3A%2F%2FM%2F9e5ef14c-b183-4307-a6e1-c0fdf559d5b6?csig=AAAAAAAAAAAAAAAAAAAAABYvq5u88tkGEvkQadcue_6AdY_e7KxdVct1etJC4glY&exp=1743750464&signer=media-rpc&token=AAIAAU0AJDllNWVmMTRjLWIxODMtNDMwNy1hNmUxLWMwZmRmNTU5ZDViNgAAAAABlf-gggDo8EPWDxB_7R8vCdmFIhCgucvH9pcq6GuRg0Kh6Eohvg", 49.99m, false, "John's Best Book", "oQWpgCUQhWfVTf3fVK6G5XdG7Z73" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
