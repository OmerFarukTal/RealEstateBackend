using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Api.Migrations
{
    /// <inheritdoc />
    public partial class ImageEntityProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Properties_PropertiesId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_PropertiesId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "PropertiesId",
                table: "Images");

            migrationBuilder.CreateIndex(
                name: "IX_Images_PropertyId",
                table: "Images",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Properties_PropertyId",
                table: "Images",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Properties_PropertyId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_PropertyId",
                table: "Images");

            migrationBuilder.AddColumn<int>(
                name: "PropertiesId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_PropertiesId",
                table: "Images",
                column: "PropertiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Properties_PropertiesId",
                table: "Images",
                column: "PropertiesId",
                principalTable: "Properties",
                principalColumn: "Id");
        }
    }
}
