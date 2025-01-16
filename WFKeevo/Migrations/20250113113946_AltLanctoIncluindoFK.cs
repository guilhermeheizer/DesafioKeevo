using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WFKeevo.Migrations
{
    public partial class AltLanctoIncluindoFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TarCodigo",
                table: "Lancto",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TarefaTarCodigo",
                table: "Lancto",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                table: "Lancto",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Lancto_TarefaTarCodigo",
                table: "Lancto",
                column: "TarefaTarCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_Lancto_UsuarioId",
                table: "Lancto",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lancto_Tarefa_TarefaTarCodigo",
                table: "Lancto",
                column: "TarefaTarCodigo",
                principalTable: "Tarefa",
                principalColumn: "TarCodigo");

            migrationBuilder.AddForeignKey(
                name: "FK_Lancto_Usuario_UsuarioId",
                table: "Lancto",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lancto_Tarefa_TarefaTarCodigo",
                table: "Lancto");

            migrationBuilder.DropForeignKey(
                name: "FK_Lancto_Usuario_UsuarioId",
                table: "Lancto");

            migrationBuilder.DropIndex(
                name: "IX_Lancto_TarefaTarCodigo",
                table: "Lancto");

            migrationBuilder.DropIndex(
                name: "IX_Lancto_UsuarioId",
                table: "Lancto");

            migrationBuilder.DropColumn(
                name: "TarCodigo",
                table: "Lancto");

            migrationBuilder.DropColumn(
                name: "TarefaTarCodigo",
                table: "Lancto");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Lancto");
        }
    }
}
