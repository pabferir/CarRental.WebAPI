#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.Infrastructure.Data.Migrations
{
    public partial class AddedCustomerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    identityNumber = table.Column<string>(type: "character varying", nullable: false),
                    name = table.Column<string>(type: "character varying", nullable: false),
                    surname = table.Column<string>(type: "character varying", nullable: false),
                    dateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    telephoneNumber = table.Column<string>(type: "character varying", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
