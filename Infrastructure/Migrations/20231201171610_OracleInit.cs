﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OracleInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LEVEL",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "VARCHAR2(100)", maxLength: 100, nullable: false),
                    DESCRIPTION = table.Column<string>(type: "VARCHAR2(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LEVEL", x => x.ID);
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
                        name: "FK_PLAYER_SCORES_LEVEL_LEVEL_ID",
                        column: x => x.LEVEL_ID,
                        principalTable: "LEVEL",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PLAYER_SCORES_PLAYERS_PLAYER_ID",
                        column: x => x.PLAYER_ID,
                        principalTable: "PLAYERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LEVEL_NAME",
                table: "LEVEL",
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
                name: "IX_PLAYER_SCORES_LEVEL_ID",
                table: "PLAYER_SCORES",
                column: "LEVEL_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PLAYER_SCORES_PLAYER_ID_LEVEL_ID",
                table: "PLAYER_SCORES",
                columns: new[] { "PLAYER_ID", "LEVEL_ID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PLAYER_SCORES");

            migrationBuilder.DropTable(
                name: "LEVEL");

            migrationBuilder.DropTable(
                name: "PLAYERS");
        }
    }
}
