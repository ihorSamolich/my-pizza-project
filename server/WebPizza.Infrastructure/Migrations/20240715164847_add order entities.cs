using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebPizza.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addorderentities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_order_status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_order_status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeliveryAddress = table.Column<string>(type: "text", nullable: false),
                    IsDelivery = table.Column<bool>(type: "boolean", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    OrderStatusId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_orders_tbl_order_status_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalTable: "tbl_order_status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_order_items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    PizzaId = table.Column<int>(type: "integer", nullable: false),
                    SizePriceId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_order_items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_order_items_tbl_orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "tbl_orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_order_items_tbl_pizza_sizes_SizePriceId",
                        column: x => x.SizePriceId,
                        principalTable: "tbl_pizza_sizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_order_items_tbl_pizzas_PizzaId",
                        column: x => x.PizzaId,
                        principalTable: "tbl_pizzas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_order_items_OrderId",
                table: "tbl_order_items",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_order_items_PizzaId",
                table: "tbl_order_items",
                column: "PizzaId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_order_items_SizePriceId",
                table: "tbl_order_items",
                column: "SizePriceId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_orders_OrderStatusId",
                table: "tbl_orders",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_orders_UserId",
                table: "tbl_orders",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_order_items");

            migrationBuilder.DropTable(
                name: "tbl_orders");

            migrationBuilder.DropTable(
                name: "tbl_order_status");
        }
    }
}
