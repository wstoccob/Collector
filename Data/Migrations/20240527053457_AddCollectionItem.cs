using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Collector.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCollectionItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    CustomInteger1State = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomInteger1Name = table.Column<string>(type: "TEXT", nullable: true),
                    CustomInteger2State = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomInteger2Name = table.Column<string>(type: "TEXT", nullable: true),
                    CustomInteger3State = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomInteger3Name = table.Column<string>(type: "TEXT", nullable: true),
                    CustomString1State = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomString1Name = table.Column<string>(type: "TEXT", nullable: true),
                    CustomString2State = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomString2Name = table.Column<string>(type: "TEXT", nullable: true),
                    CustomString3State = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomString3Name = table.Column<string>(type: "TEXT", nullable: true),
                    CustomMultilineText1State = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomMultilineText1Name = table.Column<string>(type: "TEXT", nullable: true),
                    CustomMultilineText2State = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomMultilineText2Name = table.Column<string>(type: "TEXT", nullable: true),
                    CustomMultilineText3State = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomMultilineText3Name = table.Column<string>(type: "TEXT", nullable: true),
                    CustomCheckbox1State = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomCheckbox1Name = table.Column<string>(type: "TEXT", nullable: true),
                    CustomCheckbox2State = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomCheckbox2Name = table.Column<string>(type: "TEXT", nullable: true),
                    CustomCheckbox3State = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomCheckbox3Name = table.Column<string>(type: "TEXT", nullable: true),
                    CustomDate1State = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomDate1Name = table.Column<string>(type: "TEXT", nullable: true),
                    CustomDate2State = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomDate2Name = table.Column<string>(type: "TEXT", nullable: true),
                    CustomDate3State = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomDate3Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Collections_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Collections_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CollectionId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    CustomInt1 = table.Column<int>(type: "INTEGER", nullable: true),
                    CustomInt2 = table.Column<int>(type: "INTEGER", nullable: true),
                    CustomInt3 = table.Column<int>(type: "INTEGER", nullable: true),
                    CustomString1 = table.Column<string>(type: "TEXT", nullable: true),
                    CustomString2 = table.Column<string>(type: "TEXT", nullable: true),
                    CustomString3 = table.Column<string>(type: "TEXT", nullable: true),
                    CustomMultilineText1 = table.Column<string>(type: "TEXT", nullable: true),
                    CustomMultilineText2 = table.Column<string>(type: "TEXT", nullable: true),
                    CustomMultilineText3 = table.Column<string>(type: "TEXT", nullable: true),
                    CustomCheckbox1 = table.Column<bool>(type: "INTEGER", nullable: true),
                    CustomCheckbox2 = table.Column<bool>(type: "INTEGER", nullable: true),
                    CustomCheckbox3 = table.Column<bool>(type: "INTEGER", nullable: true),
                    CustomDate1 = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    CustomDate2 = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    CustomDate3 = table.Column<DateOnly>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collections_CategoryId",
                table: "Collections",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_UserId",
                table: "Collections",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CollectionId",
                table: "Items",
                column: "CollectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Collections");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
