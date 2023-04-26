using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SFY_Word_Book.Api.Migrations
{
    /// <inheritdoc />
    public partial class Database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LearningWords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BookName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningWords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReviewWords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewWords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Account = table.Column<string>(type: "TEXT", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    userName = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Account);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HeadWord = table.Column<string>(type: "TEXT", nullable: false),
                    WordRank = table.Column<int>(type: "INTEGER", nullable: false),
                    PhoneticSymbol = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneSpeech = table.Column<string>(type: "TEXT", nullable: false),
                    Combo = table.Column<int>(type: "INTEGER", nullable: false),
                    IsLearned = table.Column<bool>(type: "INTEGER", nullable: false),
                    GroupID = table.Column<int>(type: "INTEGER", nullable: false),
                    LearningWordBookId = table.Column<int>(type: "INTEGER", nullable: true),
                    ReviewWordBookId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Words_LearningWords_LearningWordBookId",
                        column: x => x.LearningWordBookId,
                        principalTable: "LearningWords",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Words_ReviewWords_ReviewWordBookId",
                        column: x => x.ReviewWordBookId,
                        principalTable: "ReviewWords",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sentences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SentenceRank = table.Column<int>(type: "INTEGER", nullable: false),
                    WordId = table.Column<int>(type: "INTEGER", nullable: false),
                    SentenceContent = table.Column<string>(type: "TEXT", nullable: false),
                    SentenceCn = table.Column<string>(type: "TEXT", nullable: false),
                    WordRank = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sentences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sentences_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Translations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TransRank = table.Column<int>(type: "INTEGER", nullable: false),
                    WordId = table.Column<int>(type: "INTEGER", nullable: false),
                    PartOfSpeech = table.Column<string>(type: "TEXT", nullable: false),
                    TransCN = table.Column<string>(type: "TEXT", nullable: false),
                    WordRank = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Translations_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sentences_WordId",
                table: "Sentences",
                column: "WordId");

            migrationBuilder.CreateIndex(
                name: "IX_Translations_WordId",
                table: "Translations",
                column: "WordId");

            migrationBuilder.CreateIndex(
                name: "IX_Words_LearningWordBookId",
                table: "Words",
                column: "LearningWordBookId");

            migrationBuilder.CreateIndex(
                name: "IX_Words_ReviewWordBookId",
                table: "Words",
                column: "ReviewWordBookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sentences");

            migrationBuilder.DropTable(
                name: "Translations");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropTable(
                name: "LearningWords");

            migrationBuilder.DropTable(
                name: "ReviewWords");
        }
    }
}
