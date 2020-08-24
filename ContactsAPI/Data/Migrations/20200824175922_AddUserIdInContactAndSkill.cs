using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactsAPI.Data.Migrations
{
    public partial class AddUserIdInContactAndSkill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Skills",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Contacts",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_UserID",
                table: "Skills",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_UserID",
                table: "Contacts",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_AspNetUsers_UserID",
                table: "Contacts",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_AspNetUsers_UserID",
                table: "Skills",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_AspNetUsers_UserID",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_AspNetUsers_UserID",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_UserID",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_UserID",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Contacts");
        }
    }
}
