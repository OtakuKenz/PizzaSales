using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaSalesApi.Migrations
{
  /// <inheritdoc />
  public partial class InitialCreate : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "Orders",
          columns: table => new
          {
            OrderId = table.Column<int>(type: "INTEGER", nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
            Time = table.Column<TimeOnly>(type: "TEXT", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Orders", x => x.OrderId);
          });

      migrationBuilder.CreateTable(
          name: "PizzaTypes",
          columns: table => new
          {
            PizzaTypeId = table.Column<string>(type: "TEXT", nullable: false),
            Name = table.Column<string>(type: "TEXT", nullable: false),
            Category = table.Column<string>(type: "TEXT", nullable: false),
            Ingredients = table.Column<string>(type: "TEXT", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_PizzaTypes", x => x.PizzaTypeId);
          });

      migrationBuilder.CreateTable(
          name: "Pizzas",
          columns: table => new
          {
            PizzaId = table.Column<string>(type: "TEXT", nullable: false),
            PizzaTypeId = table.Column<string>(type: "TEXT", nullable: false),
            Size = table.Column<string>(type: "TEXT", nullable: false),
            Price = table.Column<decimal>(type: "TEXT", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Pizzas", x => x.PizzaId);
            table.ForeignKey(
                      name: "FK_Pizzas_PizzaTypes_PizzaTypeId",
                      column: x => x.PizzaTypeId,
                      principalTable: "PizzaTypes",
                      principalColumn: "PizzaTypeId",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "OrderDetails",
          columns: table => new
          {
            OrderDetailId = table.Column<int>(type: "INTEGER", nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            OrderId = table.Column<int>(type: "INTEGER", nullable: false),
            PizzaId = table.Column<string>(type: "TEXT", nullable: false),
            Quantity = table.Column<int>(type: "INTEGER", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailId);
            table.ForeignKey(
                      name: "FK_OrderDetails_Orders_OrderId",
                      column: x => x.OrderId,
                      principalTable: "Orders",
                      principalColumn: "OrderId",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_OrderDetails_Pizzas_PizzaId",
                      column: x => x.PizzaId,
                      principalTable: "Pizzas",
                      principalColumn: "PizzaId",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateIndex(
          name: "IX_OrderDetails_OrderId",
          table: "OrderDetails",
          column: "OrderId");

      migrationBuilder.CreateIndex(
          name: "IX_OrderDetails_PizzaId",
          table: "OrderDetails",
          column: "PizzaId");

      migrationBuilder.CreateIndex(
          name: "IX_Pizzas_PizzaTypeId",
          table: "Pizzas",
          column: "PizzaTypeId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "OrderDetails");

      migrationBuilder.DropTable(
          name: "Orders");

      migrationBuilder.DropTable(
          name: "Pizzas");

      migrationBuilder.DropTable(
          name: "PizzaTypes");
    }
  }
}
