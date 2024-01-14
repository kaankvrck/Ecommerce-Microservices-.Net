using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Services.CatalogAPI.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_catalog",
                columns: table => new
                {
                    productid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    category = table.Column<string>(type: "varchar(255)", nullable: false),
                    brand = table.Column<string>(type: "varchar(255)", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    stock = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    image = table.Column<string>(type: "varchar(255)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_catalog", x => x.productid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_catalog");
        }
    }
}
