using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVeterinaria.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIdClienteFromReclamos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdCliente",
                table: "Reclamos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdCliente",
                table: "Reclamos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
