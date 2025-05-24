using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaSalesApi.Migrations
{
    /// <inheritdoc />
    public partial class normalizedPizzaType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "PizzaTypes");

            migrationBuilder.DropColumn(
                name: "Ingredients",
                table: "PizzaTypes");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    IngredientId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.IngredientId);
                });

            migrationBuilder.CreateTable(
                name: "PizzaTypeCategories",
                columns: table => new
                {
                    PizzaTypeCategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PizzaTypeId = table.Column<string>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaTypeCategories", x => x.PizzaTypeCategoryId);
                    table.ForeignKey(
                        name: "FK_PizzaTypeCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PizzaTypeCategories_PizzaTypes_PizzaTypeId",
                        column: x => x.PizzaTypeId,
                        principalTable: "PizzaTypes",
                        principalColumn: "PizzaTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PizzaTypeIngredients",
                columns: table => new
                {
                    PizzaTypeIngredientId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PizzaTypeId = table.Column<string>(type: "TEXT", nullable: false),
                    IngredientId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaTypeIngredients", x => x.PizzaTypeIngredientId);
                    table.ForeignKey(
                        name: "FK_PizzaTypeIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PizzaTypeIngredients_PizzaTypes_PizzaTypeId",
                        column: x => x.PizzaTypeId,
                        principalTable: "PizzaTypes",
                        principalColumn: "PizzaTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PizzaTypeCategories_CategoryId",
                table: "PizzaTypeCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PizzaTypeCategories_PizzaTypeId",
                table: "PizzaTypeCategories",
                column: "PizzaTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PizzaTypeIngredients_IngredientId",
                table: "PizzaTypeIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_PizzaTypeIngredients_PizzaTypeId",
                table: "PizzaTypeIngredients",
                column: "PizzaTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PizzaTypeCategories");

            migrationBuilder.DropTable(
                name: "PizzaTypeIngredients");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "PizzaTypes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Ingredients",
                table: "PizzaTypes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
