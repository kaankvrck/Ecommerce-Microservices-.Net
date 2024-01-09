using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Ecommerce.Services.OrderAPI.Migrations
{
    public partial class orderstructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_order",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    customerid = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    surname = table.Column<string>(type: "text", nullable: true),
                    phonenumber = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    totalprice = table.Column<decimal>(type: "numeric", nullable: false),
                    statusid = table.Column<int>(type: "integer", nullable: false),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<int>(type: "integer", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<int>(type: "integer", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_order", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_order_detail",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    orderid = table.Column<int>(type: "integer", nullable: false),
                    productid = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<int>(type: "integer", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<int>(type: "integer", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_order_detail", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_order_status",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "text", nullable: false),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<int>(type: "integer", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<int>(type: "integer", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_order_status", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_order");

            migrationBuilder.DropTable(
                name: "tb_order_detail");

            migrationBuilder.DropTable(
                name: "tb_order_status");
        }
    }
}
