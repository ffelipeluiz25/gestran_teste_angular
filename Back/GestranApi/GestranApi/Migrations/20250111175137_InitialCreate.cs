using Microsoft.EntityFrameworkCore.Migrations;
namespace GestranApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "TipoUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoUsuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCompleto = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdTipoUsuario = table.Column<int>(type: "int", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Checklist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
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
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IdUsuarioAlteracao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Item_Usuario_IdUsuarioAlteracao",
                        column: x => x.IdUsuarioAlteracao,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdChecklist = table.Column<int>(type: "int", nullable: false),
                    IdItem = table.Column<int>(type: "int", nullable: false),
                    IdUsuarioAlteracao = table.Column<int>(type: "int", nullable: false),
                    Executado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistItem_Checklist_IdChecklist",
                        column: x => x.IdChecklist,
                        principalTable: "Checklist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChecklistItem_Item_IdItem",
                        column: x => x.IdItem,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChecklistItem_Usuario_IdUsuarioAlteracao",
                        column: x => x.IdUsuarioAlteracao,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItem_IdChecklist",
                table: "ChecklistItem",
                column: "IdChecklist");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItem_IdItem",
                table: "ChecklistItem",
                column: "IdItem");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItem_IdUsuarioAlteracao",
                table: "ChecklistItem",
                column: "IdUsuarioAlteracao");

            migrationBuilder.CreateIndex(
                name: "IX_Item_IdUsuarioAlteracao",
                table: "Item",
                column: "IdUsuarioAlteracao");

            migrationBuilder.Sql("INSERT INTO TipoUsuario (Descricao) VALUES ('Supervisor');");
            migrationBuilder.Sql("INSERT INTO TipoUsuario (Descricao) VALUES ('Executor');");

            migrationBuilder.Sql(@"INSERT INTO Usuario (NomeCompleto,IdTipoUsuario,Login,Senha) VALUES ('Supervisor',1,'Supervisor','e10adc3949ba59abbe56e057f20f883e');");
            migrationBuilder.Sql(@"INSERT INTO Usuario (NomeCompleto,IdTipoUsuario,Login,Senha) VALUES ('Executor 1',2,'Executor1','e10adc3949ba59abbe56e057f20f883e');");
            migrationBuilder.Sql(@"INSERT INTO Usuario (NomeCompleto,IdTipoUsuario,Login,Senha) VALUES ('Executor 2',2,'Executor2','e10adc3949ba59abbe56e057f20f883e');");

            migrationBuilder.Sql("INSERT INTO Status (Nome) VALUES ('PENDENTE');");
            migrationBuilder.Sql("INSERT INTO Status (Nome) VALUES ('EXECUTANDO');");
            migrationBuilder.Sql("INSERT INTO Status (Nome) VALUES ('FINALIZADO EXECUTOR');");
            migrationBuilder.Sql("INSERT INTO Status (Nome) VALUES ('APROVADO SUPERVISOR');");
            migrationBuilder.Sql("INSERT INTO Status (Nome) VALUES ('REPROVADO SUPERVISOR');");

            migrationBuilder.Sql("INSERT INTO Item (Nome, Observacao, IdUsuarioAlteracao) VALUES ('Pintura','Verificar estado da pintura e lataria',1);");
            migrationBuilder.Sql("INSERT INTO Item (Nome, Observacao, IdUsuarioAlteracao) VALUES ('Pneus','Inspecionar os pneus (calibragem, desgaste, bolhas ou cortes)',1);");
            migrationBuilder.Sql("INSERT INTO Item (Nome, Observacao, IdUsuarioAlteracao) VALUES ('Estepe','Conferir estado do estepe',1);");
            migrationBuilder.Sql("INSERT INTO Item (Nome, Observacao, IdUsuarioAlteracao) VALUES ('Luzes','Checar luz baixa, alta e de neblina, testar luzes de freio e ré, inspecionar luzes de seta e alerta e conferir luz interna',1);");
            migrationBuilder.Sql("INSERT INTO Item (Nome, Observacao, IdUsuarioAlteracao) VALUES ('Motor','Verificar nível de óleo do motor e conferir nível e qualidade do líquido de arrefecimento',1);");
            migrationBuilder.Sql("INSERT INTO Item (Nome, Observacao, IdUsuarioAlteracao) VALUES ('Interior do Veículo','Conferir funcionamento do painel de instrumentos, testar ar-condicionado, ventilação e limpar interior e higienizar bancos',1);");

            migrationBuilder.Sql("INSERT INTO Checklist (Descricao,IdUsuarioExecutor,IdStatus,IdUsuarioAlteracao) VALUES('Checklist 1',null,1,1);");
            migrationBuilder.Sql("INSERT INTO Checklist (Descricao,IdUsuarioExecutor,IdStatus,IdUsuarioAlteracao) VALUES('Checklist 2',null,1,1);");

            migrationBuilder.Sql("INSERT INTO ChecklistItem (IdChecklist,IdItem,IdUsuarioAlteracao,Executado) VALUES(1,1,1,0);");
            migrationBuilder.Sql("INSERT INTO ChecklistItem (IdChecklist,IdItem,IdUsuarioAlteracao,Executado) VALUES(1,2,1,0);");
            migrationBuilder.Sql("INSERT INTO ChecklistItem (IdChecklist,IdItem,IdUsuarioAlteracao,Executado) VALUES(1,3,1,0);");

            migrationBuilder.Sql("INSERT INTO ChecklistItem (IdChecklist,IdItem,IdUsuarioAlteracao,Executado) VALUES(2,1,1,0);");
            migrationBuilder.Sql("INSERT INTO ChecklistItem (IdChecklist,IdItem,IdUsuarioAlteracao,Executado) VALUES(2,2,1,0);");
            migrationBuilder.Sql("INSERT INTO ChecklistItem (IdChecklist,IdItem,IdUsuarioAlteracao,Executado) VALUES(2,3,1,0);");
            migrationBuilder.Sql("INSERT INTO ChecklistItem (IdChecklist,IdItem,IdUsuarioAlteracao,Executado) VALUES(2,4,1,0);");
            migrationBuilder.Sql("INSERT INTO ChecklistItem (IdChecklist,IdItem,IdUsuarioAlteracao,Executado) VALUES(2,5,1,0);");
            migrationBuilder.Sql("INSERT INTO ChecklistItem (IdChecklist,IdItem,IdUsuarioAlteracao,Executado) VALUES(2,6,1,0);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChecklistItem");

            migrationBuilder.DropTable(
                name: "TipoUsuario");

            migrationBuilder.DropTable(
                name: "Checklist");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}