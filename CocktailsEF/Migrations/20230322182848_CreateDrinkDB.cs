using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace CocktailsEF.Migrations
{
    /// <inheritdoc />
    public partial class CreateDrinkDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Amounts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<float>(type: "float", nullable: false),
                    Unite = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amounts", x => x.ID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Drinks",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drinks", x => x.ID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Icon = table.Column<string>(type: "longtext", nullable: false),
                    Discriminator = table.Column<string>(type: "longtext", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: true),
                    Color = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.ID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AmountIngredients",
                columns: table => new
                {
                    IngredientID = table.Column<int>(type: "int", nullable: false),
                    AmountID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmountIngredients", x => new { x.IngredientID, x.AmountID });
                    table.ForeignKey(
                        name: "FK_AmountIngredients_Amounts_AmountID",
                        column: x => x.AmountID,
                        principalTable: "Amounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AmountIngredients_Ingredient_IngredientID",
                        column: x => x.IngredientID,
                        principalTable: "Ingredient",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AmountIngredientDrink",
                columns: table => new
                {
                    DrinksID = table.Column<int>(type: "int", nullable: false),
                    AmountIngredientIngredientID = table.Column<int>(type: "int", nullable: false),
                    AmountIngredientAmountID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmountIngredientDrink", x => new { x.DrinksID, x.AmountIngredientIngredientID, x.AmountIngredientAmountID });
                    table.ForeignKey(
                        name: "FK_AmountIngredientDrink_AmountIngredients_AmountIngredientIngr~",
                        columns: x => new { x.AmountIngredientIngredientID, x.AmountIngredientAmountID },
                        principalTable: "AmountIngredients",
                        principalColumns: new[] { "IngredientID", "AmountID" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AmountIngredientDrink_Drinks_DrinksID",
                        column: x => x.DrinksID,
                        principalTable: "Drinks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AmountIngredientDrink_AmountIngredientIngredientID_AmountIng~",
                table: "AmountIngredientDrink",
                columns: new[] { "AmountIngredientIngredientID", "AmountIngredientAmountID" });

            migrationBuilder.CreateIndex(
                name: "IX_AmountIngredients_AmountID",
                table: "AmountIngredients",
                column: "AmountID");

            migrationBuilder.CreateIndex(
                name: "IX_Amounts_ID",
                table: "Amounts",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drinks_ID",
                table: "Drinks",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_ID",
                table: "Ingredient",
                column: "ID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmountIngredientDrink");

            migrationBuilder.DropTable(
                name: "AmountIngredients");

            migrationBuilder.DropTable(
                name: "Drinks");

            migrationBuilder.DropTable(
                name: "Amounts");

            migrationBuilder.DropTable(
                name: "Ingredient");
        }
    }
}
