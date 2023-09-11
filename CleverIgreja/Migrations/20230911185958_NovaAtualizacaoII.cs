using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleverIgreja.Migrations
{
    /// <inheritdoc />
    public partial class NovaAtualizacaoII : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Membro",
                columns: table => new
                {
                    MembroId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoPessoa = table.Column<int>(type: "int", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CnpjCpf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RgIe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UF = table.Column<int>(type: "int", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Complemento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Celular = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Whatsapp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membro", x => x.MembroId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Membro");
        }
    }
}
