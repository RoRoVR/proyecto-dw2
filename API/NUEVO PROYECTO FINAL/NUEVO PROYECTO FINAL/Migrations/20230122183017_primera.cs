using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NUEVO_PROYECTO_FINAL.Migrations
{
    public partial class primera : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "actores",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    foto = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_actores", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "directores",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    foto = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_directores", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "generos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_generos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "peliculas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    titulo_original = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    anio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    duracion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sinopsis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    portada = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    estado = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_peliculas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    foto = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "actor_pelicula",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    peliculaid = table.Column<int>(type: "int", nullable: false),
                    actorid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_actor_pelicula", x => x.id);
                    table.ForeignKey(
                        name: "FK_actor_pelicula_actores_actorid",
                        column: x => x.actorid,
                        principalTable: "actores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_actor_pelicula_peliculas_peliculaid",
                        column: x => x.peliculaid,
                        principalTable: "peliculas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "director_pelicula",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    peliculaid = table.Column<int>(type: "int", nullable: false),
                    directorid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_director_pelicula", x => x.id);
                    table.ForeignKey(
                        name: "FK_director_pelicula_directores_directorid",
                        column: x => x.directorid,
                        principalTable: "directores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_director_pelicula_peliculas_peliculaid",
                        column: x => x.peliculaid,
                        principalTable: "peliculas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pelicula_genero",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    peliculaid = table.Column<int>(type: "int", nullable: false),
                    generoid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pelicula_genero", x => x.id);
                    table.ForeignKey(
                        name: "FK_pelicula_genero_generos_generoid",
                        column: x => x.generoid,
                        principalTable: "generos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pelicula_genero_peliculas_peliculaid",
                        column: x => x.peliculaid,
                        principalTable: "peliculas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_actor_pelicula_actorid",
                table: "actor_pelicula",
                column: "actorid");

            migrationBuilder.CreateIndex(
                name: "IX_actor_pelicula_peliculaid",
                table: "actor_pelicula",
                column: "peliculaid");

            migrationBuilder.CreateIndex(
                name: "IX_director_pelicula_directorid",
                table: "director_pelicula",
                column: "directorid");

            migrationBuilder.CreateIndex(
                name: "IX_director_pelicula_peliculaid",
                table: "director_pelicula",
                column: "peliculaid");

            migrationBuilder.CreateIndex(
                name: "IX_pelicula_genero_generoid",
                table: "pelicula_genero",
                column: "generoid");

            migrationBuilder.CreateIndex(
                name: "IX_pelicula_genero_peliculaid",
                table: "pelicula_genero",
                column: "peliculaid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "actor_pelicula");

            migrationBuilder.DropTable(
                name: "director_pelicula");

            migrationBuilder.DropTable(
                name: "pelicula_genero");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "actores");

            migrationBuilder.DropTable(
                name: "directores");

            migrationBuilder.DropTable(
                name: "generos");

            migrationBuilder.DropTable(
                name: "peliculas");
        }
    }
}
