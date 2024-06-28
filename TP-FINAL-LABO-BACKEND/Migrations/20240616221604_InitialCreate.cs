using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace TP_FINAL_LABO_BACKEND.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    IdRole = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameRole = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.IdRole);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameUser = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUser = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    IdPost = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedPost = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedPost = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedPost = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.IdPost);
                    table.ForeignKey(
                        name: "FK_Post_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleUsers",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUsers", x => new { x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_RoleUsers_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "IdRole",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUsers_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    IdComment = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IdPost = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedComment = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedComment = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedComment = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.IdComment);
                    table.ForeignKey(
                        name: "FK_Comment_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_Post_IdPost",
                        column: x => x.IdPost,
                        principalTable: "Post",
                        principalColumn: "IdPost",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
         name: "Likes",
         columns: table => new
         {
             Id = table.Column<int>(type: "int", nullable: false)
                 .Annotation("SqlServer:Identity", "1, 1"),
             PostId = table.Column<int>(type: "int", nullable: false),
             UserId = table.Column<int>(type: "int", nullable: false),
             CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
         },
         constraints: table =>
         {
             table.PrimaryKey("PK_Likes", x => x.Id);
             table.ForeignKey(
                 name: "FK_Likes_Post_PostId",
                 column: x => x.PostId,
                 principalTable: "Post",
                 principalColumn: "IdPost",
                 onDelete: ReferentialAction.NoAction); // Cambia ReferentialAction.Cascade a ReferentialAction.NoAction
             table.ForeignKey(
                 name: "FK_Likes_User_UserId",
                 column: x => x.UserId,
                 principalTable: "User",
                 principalColumn: "UserId",
                 onDelete: ReferentialAction.NoAction); // Cambia ReferentialAction.Cascade a ReferentialAction.NoAction
         });


            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "IdRole", "NameRole" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "User" },
                    { 3, "Moderator" }
                });

            string salt = BCrypt.Net.BCrypt.GenerateSalt(13);
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword("1234567", salt);

            migrationBuilder.InsertData(
            table: "User",
            columns: new[] { "UserId", "NameUser", "Username", "PasswordUser", "Email", "CreatedUser", "Birthdate" },
            values: new object[,]
            {
                { 1, "Juan Perez", "juanperez", hashedPassword, "juan.perez@example.com", DateTime.Now, new DateTime(1990, 5, 10)},
                { 2, "Maria Garcia", "mgarcia", hashedPassword, "maria.garcia@example.com", DateTime.Now, new DateTime(1985, 8, 25)},
                { 3, "Carlos Hernandez", "carlos_h", hashedPassword, "carlos.hernandez@example.com", DateTime.Now, new DateTime(1992, 2, 14)},
                { 4, "Ana Martinez", "ana_m", hashedPassword, "ana.martinez@example.com", DateTime.Now, new DateTime(1995, 12, 30)},
                { 5, "Luis Gomez", "luis_g", hashedPassword, "luis.gomez@example.com", DateTime.Now, new DateTime(1988, 7, 20)},
                { 6, "Lionel Messi", "lmessi", hashedPassword, "lmessi@mail.com", DateTime.Now, new DateTime(1987, 6, 24) },
                { 7, "Elon Musk", "emusk", hashedPassword, "emusk@mail.com", DateTime.Now, new DateTime(1971, 6, 28) }
            });

            migrationBuilder.InsertData(
            table: "Post",
            columns: new[] { "IdPost", "UserId", "Title", "Content", "CreatedPost" },
            values: new object[,]
            {
                { 1, 1, "Cómo Aprender a Programar", "Aprender a programar puede ser un desafío, pero con la práctica constante y el uso de recursos adecuados, es posible dominar cualquier lenguaje.", DateTime.Now},
                { 2, 2, "Recetas Saludables para el Desayuno", "Comenzar el día con un desayuno saludable es crucial. Aquí tienes algunas recetas rápidas y fáciles para mantenerte enérgico toda la mañana.", DateTime.Now},
                { 3, 3, "Guía de Viaje: Explorando Japón", "Japón es un país lleno de historia y cultura. En esta guía, exploraremos los mejores lugares para visitar y cómo disfrutar al máximo tu viaje.", DateTime.Now},
                { 4, 4, "Estrategias de Marketing Digital para 2024", "El marketing digital está en constante evolución. Descubre las estrategias más efectivas para el próximo año y cómo implementarlas en tu negocio.", DateTime.Now},
                { 5, 5, "Los Beneficios del Ejercicio Diario", "Hacer ejercicio diariamente tiene numerosos beneficios para la salud física y mental. Aquí te contamos algunos de los más importantes.", DateTime.Now},
                { 6, 1, "Tecnologías Emergentes en 2024", "El 2024 trae consigo una serie de tecnologías emergentes que prometen transformar diversas industrias. Descubre cuáles son y cómo pueden impactar tu vida.", DateTime.Now},
                { 7, 2, "Cómo Manejar el Estrés en el Trabajo", "El estrés laboral es un problema común en la vida moderna. Aquí te ofrecemos algunas estrategias efectivas para manejar el estrés y mejorar tu bienestar en el trabajo.", DateTime.Now}
            });

            var userRoles = new object[,]
            {
                { 3, 1 }, // Juan Perez - Moderator
                { 3, 2 }, // Maria Garcia - Moderator
                { 2, 3 }, // Carlos Hernandez - User
                { 2, 4 }, // Ana Martinez - User
                { 2, 5 }, // Luis Gomez - User
                { 2, 6 }, // Lionel Messi - User
                { 2, 7 }  // Elon Musk - User
            };

            migrationBuilder.InsertData(
                table: "RoleUsers",
                columns: new[] { "RoleId", "UserId" },
                values: userRoles);

            migrationBuilder.CreateIndex(
                name: "IX_Post_Title",
                table: "Post",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Post_UserId",
                table: "Post",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUsers_UserId",
                table: "RoleUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                table: "User",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UserId",
                table: "Comment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_IdPost",
                table: "Comment",
                column: "IdPost");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_PostId",
                table: "Likes",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserId",
                table: "Likes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "RoleUsers");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
