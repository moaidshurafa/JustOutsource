using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JustOutsource.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addImageUrlToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Freelancers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Freelancers");
        }
    }
}
