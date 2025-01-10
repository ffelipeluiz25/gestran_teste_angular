using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestranApi.Migrations
{
    /// <inheritdoc />
    public partial class InsertTipoUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Senha",
                table: "Usuario",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.Sql("INSERT INTO TipoUsuario (Descricao) VALUES ('Servidor');");
            migrationBuilder.Sql("INSERT INTO TipoUsuario (Descricao) VALUES ('Executor');");
            migrationBuilder.Sql(@"INSERT INTO Usuario (NomeCompleto,IdTipoUsuario,Login,Senha) 
                            VALUES ('Servidor',2,'Servidor','e10adc3949ba59abbe56e057f20f883e');");
            migrationBuilder.Sql(@"INSERT INTO Usuario (NomeCompleto,IdTipoUsuario,Login,Senha) 
                            VALUES ('Executor 1',2,'Executor1','e10adc3949ba59abbe56e057f20f883e');");
            migrationBuilder.Sql(@"INSERT INTO Usuario (NomeCompleto,IdTipoUsuario,Login,Senha) 
                            VALUES ('Executor 2',2,'Executor2','e10adc3949ba59abbe56e057f20f883e');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Senha",
                table: "Usuario",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.Sql("DELETE FROM Usuario WHERE Id > 0;");
            migrationBuilder.Sql("DELETE FROM TipoUsuario WHERE Id > 0;");
        }
    }
}