using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiGeo.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddressHistory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "varchar(100)", nullable: true),
                    Province = table.Column<string>(type: "varchar(100)", nullable: true),
                    ZipCode = table.Column<string>(type: "varchar(20)", nullable: true),
                    City = table.Column<string>(type: "varchar(100)", nullable: true),
                    StreetNumber = table.Column<string>(type: "varchar(20)", nullable: true),
                    Street = table.Column<string>(type: "varchar(200)", nullable: true),
                    latitud = table.Column<string>(type: "varchar(200)", nullable: true),
                    longitude = table.Column<string>(type: "varchar(200)", nullable: true),
                    status = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressHistory", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddressHistory");
        }
    }
}
