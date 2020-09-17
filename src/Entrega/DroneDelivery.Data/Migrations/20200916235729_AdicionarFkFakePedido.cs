using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DroneDelivery.Data.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class AdicionarFkFakePedido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PedidoId",
                schema: "dbo",
                table: "HistoricoPedidos",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PedidoId",
                schema: "dbo",
                table: "HistoricoPedidos");
        }
    }
}
