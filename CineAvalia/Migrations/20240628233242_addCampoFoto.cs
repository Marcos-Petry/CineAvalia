using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CineAvalia.Migrations
{
    /// <inheritdoc />
    public partial class addCampoFoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Imagem",
                table: "Filme",
                type: "bytea",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagem",
                table: "Filme");
        }
    }
}
