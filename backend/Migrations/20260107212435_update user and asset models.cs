using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class updateuserandassetmodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Publisher_UserId",
                table: "Assets");

            migrationBuilder.RenameColumn(
                name: "Publisher_PhoneNumber",
                table: "Assets",
                newName: "ContactDetails_PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Publisher_FullName",
                table: "Assets",
                newName: "ContactDetails_FullName");

            migrationBuilder.AddColumn<string>(
                name: "PublisherId",
                table: "Assets",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_PublisherId",
                table: "Assets",
                column: "PublisherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_AspNetUsers_PublisherId",
                table: "Assets",
                column: "PublisherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_AspNetUsers_PublisherId",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_PublisherId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "PublisherId",
                table: "Assets");

            migrationBuilder.RenameColumn(
                name: "ContactDetails_PhoneNumber",
                table: "Assets",
                newName: "Publisher_PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "ContactDetails_FullName",
                table: "Assets",
                newName: "Publisher_FullName");

            migrationBuilder.AddColumn<string>(
                name: "Publisher_UserId",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
