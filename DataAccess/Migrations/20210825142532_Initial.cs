using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbadmin");

            migrationBuilder.CreateTable(
                name: "authors",
                schema: "dbadmin",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    email = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    join_date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("authors_pk", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                schema: "dbadmin",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tags_pk", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                },
                comment: "desc tags for articles");

            migrationBuilder.CreateTable(
                name: "articles",
                schema: "dbadmin",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    publish_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    id_author = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("articles_pk", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "articles_authors_id_fk",
                        column: x => x.id_author,
                        principalSchema: "dbadmin",
                        principalTable: "authors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "article_tags",
                schema: "dbadmin",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_article = table.Column<int>(type: "int", nullable: true),
                    id_tag = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("article_tags_pk", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "article_tags_articles_id_fk",
                        column: x => x.id_article,
                        principalSchema: "dbadmin",
                        principalTable: "articles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "article_tags_tags_id_fk",
                        column: x => x.id_tag,
                        principalSchema: "dbadmin",
                        principalTable: "tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "texts",
                schema: "dbadmin",
                columns: table => new
                {
                    id_article = table.Column<int>(type: "int", nullable: false),
                    reader_rating = table.Column<int>(type: "int", nullable: true),
                    adult_only = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    data = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "texts_articles_id_fk",
                        column: x => x.id_article,
                        principalSchema: "dbadmin",
                        principalTable: "articles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_article_tags_id_article",
                schema: "dbadmin",
                table: "article_tags",
                column: "id_article");

            migrationBuilder.CreateIndex(
                name: "IX_article_tags_id_tag",
                schema: "dbadmin",
                table: "article_tags",
                column: "id_tag");

            migrationBuilder.CreateIndex(
                name: "IX_articles_id_author",
                schema: "dbadmin",
                table: "articles",
                column: "id_author");

            migrationBuilder.CreateIndex(
                name: "texts_id_article_uindex",
                schema: "dbadmin",
                table: "texts",
                column: "id_article",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "article_tags",
                schema: "dbadmin");

            migrationBuilder.DropTable(
                name: "texts",
                schema: "dbadmin");

            migrationBuilder.DropTable(
                name: "tags",
                schema: "dbadmin");

            migrationBuilder.DropTable(
                name: "articles",
                schema: "dbadmin");

            migrationBuilder.DropTable(
                name: "authors",
                schema: "dbadmin");
        }
    }
}
