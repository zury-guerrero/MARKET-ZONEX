using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace MARKET_PLACE.Migrations
{
    public partial class zury : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    PkGenero = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NombreG = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.PkGenero);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    PkRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.PkRol);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    PkProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Precio = table.Column<double>(type: "double", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    ImagenPath = table.Column<string>(type: "text", nullable: false),
                    FkGenero = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.PkProducto);
                    table.ForeignKey(
                        name: "FK_Productos_Generos_FkGenero",
                        column: x => x.FkGenero,
                        principalTable: "Generos",
                        principalColumn: "PkGenero",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    PkUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    FkRol = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.PkUsuario);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_FkRol",
                        column: x => x.FkRol,
                        principalTable: "Roles",
                        principalColumn: "PkRol",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "PkGenero", "NombreG" },
                values: new object[,]
                {
                    { 1, "Electrónicos" },
                    { 2, "Moda" },
                    { 3, "Belleza y cuidado personal" },
                    { 4, "Deportes" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "PkRol", "Nombre" },
                values: new object[,]
                {
                    { 1, "Administrador" },
                    { 2, "Vendedor" },
                    { 3, "Comprador" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "PkUsuario", "FkRol", "Nombre", "Password", "UserName" },
                values: new object[] { 1, 1, "Diego", "1234", "Admin1" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "PkUsuario", "FkRol", "Nombre", "Password", "UserName" },
                values: new object[] { 2, 2, "Maximiliano", "1212", "User1" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "PkUsuario", "FkRol", "Nombre", "Password", "UserName" },
                values: new object[] { 3, 3, "Adamaris", "pato", "User2" });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_FkGenero",
                table: "Productos",
                column: "FkGenero");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_FkRol",
                table: "Usuarios",
                column: "FkRol");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Generos");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
