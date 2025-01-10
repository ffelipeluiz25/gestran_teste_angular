using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestranApi.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableInfra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Checklist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuarioExecutor = table.Column<int>(type: "int", nullable: true),
                    IdStatus = table.Column<int>(type: "int", nullable: false),
                    IdUsuarioAlteracao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checklist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Checklist_Status_IdStatus",
                        column: x => x.IdStatus,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Checklist_Usuario_IdUsuarioAlteracao",
                        column: x => x.IdUsuarioAlteracao,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Checklist_Usuario_IdUsuarioExecutor",
                        column: x => x.IdUsuarioExecutor,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChecklisItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdChecklist = table.Column<int>(type: "int", nullable: false),
                    IdItem = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklisItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklisItem_Checklist_IdChecklist",
                        column: x => x.IdChecklist,
                        principalTable: "Checklist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChecklisItem_Item_IdItem",
                        column: x => x.IdItem,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChecklisItem_IdChecklist",
                table: "ChecklisItem",
                column: "IdChecklist");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklisItem_IdItem",
                table: "ChecklisItem",
                column: "IdItem");

            migrationBuilder.CreateIndex(
                name: "IX_Checklist_IdStatus",
                table: "Checklist",
                column: "IdStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Checklist_IdUsuarioAlteracao",
                table: "Checklist",
                column: "IdUsuarioAlteracao");

            migrationBuilder.CreateIndex(
                name: "IX_Checklist_IdUsuarioExecutor",
                table: "Checklist",
                column: "IdUsuarioExecutor");

            migrationBuilder.Sql("INSERT INTO Status (Nome) VALUES ('PENDENTE');");
            migrationBuilder.Sql("INSERT INTO Status (Nome) VALUES ('EXECUTANDO');");
            migrationBuilder.Sql("INSERT INTO Status (Nome) VALUES ('FINALIZADO EXECUTOR');");
            migrationBuilder.Sql("INSERT INTO Status (Nome) VALUES ('APROVADO SUPERVISOR');");
            migrationBuilder.Sql("INSERT INTO Status (Nome) VALUES ('REPROVADO SUPERVISOR');");

            migrationBuilder.Sql("INSERT INTO Item (Nome, Observacao) VALUES ('Pintura','Verificar estado da pintura e lataria');");
            migrationBuilder.Sql("INSERT INTO Item (Nome, Observacao) VALUES ('Pneus','Inspecionar os pneus (calibragem, desgaste, bolhas ou cortes)');");
            migrationBuilder.Sql("INSERT INTO Item (Nome, Observacao) VALUES ('Estepe','Conferir estado do estepe');");
            migrationBuilder.Sql("INSERT INTO Item (Nome, Observacao) VALUES ('Luzes','Checar luz baixa, alta e de neblina, testar luzes de freio e ré, inspecionar luzes de seta e alerta e conferir luz interna');");
            migrationBuilder.Sql("INSERT INTO Item (Nome, Observacao) VALUES ('Motor','Verificar nível de óleo do motor e conferir nível e qualidade do líquido de arrefecimento');");
            migrationBuilder.Sql("INSERT INTO Item (Nome, Observacao) VALUES ('Interior do Veículo','Conferir funcionamento do painel de instrumentos, testar ar-condicionado, ventilação e limpar interior e higienizar bancos');");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChecklisItem");

            migrationBuilder.DropTable(
                name: "Checklist");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Status");
        }
    }
}
