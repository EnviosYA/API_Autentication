using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PS.Template.AccessData.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estado",
                columns: table => new
                {
                    IdEstado = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescEstado = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Estado__FBB0EDC12929C298", x => x.IdEstado);
                });

            migrationBuilder.CreateTable(
                name: "TipoCuenta",
                columns: table => new
                {
                    IdTipoCuenta = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescTipCuenta = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TipoCuen__9CCA28505748AB74", x => x.IdTipoCuenta);
                });

            migrationBuilder.CreateTable(
                name: "Cuenta",
                columns: table => new
                {
                    IdCuenta = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mail = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Contraseña = table.Column<string>(nullable: false),
                    IdEstado = table.Column<int>(nullable: false),
                    IdUsuario = table.Column<int>(nullable: false),
                    IdTipoCuenta = table.Column<int>(nullable: false),
                    FecAlta = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cuenta__D41FD70632A42B05", x => x.IdCuenta);
                    table.ForeignKey(
                        name: "FK_Cuenta_Estado",
                        column: x => x.IdEstado,
                        principalTable: "Estado",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cuenta_TipoCuenta",
                        column: x => x.IdTipoCuenta,
                        principalTable: "TipoCuenta",
                        principalColumn: "IdTipoCuenta",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Estado",
                columns: new[] { "IdEstado", "DescEstado" },
                values: new object[,]
                {
                    { 1, "Alta" },
                    { 2, "Baja" }
                });

            migrationBuilder.InsertData(
                table: "TipoCuenta",
                columns: new[] { "IdTipoCuenta", "DescTipCuenta" },
                values: new object[,]
                {
                    { 1, "Usuario" },
                    { 2, "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cuenta_IdEstado",
                table: "Cuenta",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_Cuenta_IdTipoCuenta",
                table: "Cuenta",
                column: "IdTipoCuenta");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cuenta");

            migrationBuilder.DropTable(
                name: "Estado");

            migrationBuilder.DropTable(
                name: "TipoCuenta");
        }
    }
}
