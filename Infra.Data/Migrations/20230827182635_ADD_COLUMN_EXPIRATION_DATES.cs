using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class ADD_COLUMN_EXPIRATION_DATES : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "CreditCards");

            migrationBuilder.AddColumn<string>(
                name: "ExpirationMonth",
                table: "CreditCards",
                type: "char(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExpirationYear",
                table: "CreditCards",
                type: "char(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpirationMonth",
                table: "CreditCards");

            migrationBuilder.DropColumn(
                name: "ExpirationYear",
                table: "CreditCards");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "CreditCards",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
