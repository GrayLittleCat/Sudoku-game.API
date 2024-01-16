using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OracleInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LEVELS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    NAME = table.Column<string>(type: "VARCHAR2(100)", maxLength: 100, nullable: false),
                    DESCRIPTION = table.Column<string>(type: "VARCHAR2(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LEVELS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PERMISSIONS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERMISSIONS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PLAYERS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    EMAIL = table.Column<string>(type: "VARCHAR2(255)", maxLength: 255, nullable: false),
                    NICKNAME = table.Column<string>(type: "VARCHAR2(100)", maxLength: 100, nullable: false),
                    IDENTITY_ID = table.Column<string>(type: "VARCHAR2(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PLAYERS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ROLES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PLAYER_SCORES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    PLAYER_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    LEVEL_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    SCORE = table.Column<decimal>(type: "NUMBER(38,17)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PLAYER_SCORES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PLAYER_SCORES_LEVELS_LEVEL_ID",
                        column: x => x.LEVEL_ID,
                        principalTable: "LEVELS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PLAYER_SCORES_PLAYERS_PLAYER_ID",
                        column: x => x.PLAYER_ID,
                        principalTable: "PLAYERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PLAYER_ROLES",
                columns: table => new
                {
                    PLAYER_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ROLE_ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PLAYER_ROLES", x => new { x.PLAYER_ID, x.ROLE_ID });
                    table.ForeignKey(
                        name: "FK_PLAYER_ROLES_PLAYERS_PLAYER_ID",
                        column: x => x.PLAYER_ID,
                        principalTable: "PLAYERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PLAYER_ROLES_ROLES_ROLE_ID",
                        column: x => x.ROLE_ID,
                        principalTable: "ROLES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ROLE_PERMISSIONS",
                columns: table => new
                {
                    ROLE_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    PERMISSION_ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLE_PERMISSIONS", x => new { x.ROLE_ID, x.PERMISSION_ID });
                    table.ForeignKey(
                        name: "FK_ROLE_PERMISSIONS_PERMISSIONS_PERMISSION_ID",
                        column: x => x.PERMISSION_ID,
                        principalTable: "PERMISSIONS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ROLE_PERMISSIONS_ROLES_ROLE_ID",
                        column: x => x.ROLE_ID,
                        principalTable: "ROLES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "LEVELS",
                columns: new[] { "ID", "DESCRIPTION", "NAME" },
                values: new object[,]
                {
                    { 1, null, "Easy" },
                    { 2, null, "Medium" },
                    { 3, null, "Hard" }
                });

            migrationBuilder.InsertData(
                table: "PERMISSIONS",
                columns: new[] { "ID", "NAME" },
                values: new object[,]
                {
                    { 1, "ReadMember" },
                    { 2, "UpdateMember" }
                });

            migrationBuilder.InsertData(
                table: "ROLES",
                columns: new[] { "ID", "NAME" },
                values: new object[] { 1, "Registered" });

            migrationBuilder.InsertData(
                table: "ROLE_PERMISSIONS",
                columns: new[] { "PERMISSION_ID", "ROLE_ID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LEVELS_NAME",
                table: "LEVELS",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PLAYERS_EMAIL",
                table: "PLAYERS",
                column: "EMAIL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PLAYERS_IDENTITY_ID",
                table: "PLAYERS",
                column: "IDENTITY_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PLAYER_ROLES_ROLE_ID",
                table: "PLAYER_ROLES",
                column: "ROLE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PLAYER_SCORES_LEVEL_ID",
                table: "PLAYER_SCORES",
                column: "LEVEL_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PLAYER_SCORES_PLAYER_ID_LEVEL_ID",
                table: "PLAYER_SCORES",
                columns: new[] { "PLAYER_ID", "LEVEL_ID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ROLE_PERMISSIONS_PERMISSION_ID",
                table: "ROLE_PERMISSIONS",
                column: "PERMISSION_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PLAYER_ROLES");

            migrationBuilder.DropTable(
                name: "PLAYER_SCORES");

            migrationBuilder.DropTable(
                name: "ROLE_PERMISSIONS");

            migrationBuilder.DropTable(
                name: "LEVELS");

            migrationBuilder.DropTable(
                name: "PLAYERS");

            migrationBuilder.DropTable(
                name: "PERMISSIONS");

            migrationBuilder.DropTable(
                name: "ROLES");
        }
    }
}
