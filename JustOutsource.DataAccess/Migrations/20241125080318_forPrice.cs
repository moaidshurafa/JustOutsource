using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JustOutsource.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class forPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ListPrice",
                table: "Freelancers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListPrice",
                table: "Freelancers");
        }
    }
}
