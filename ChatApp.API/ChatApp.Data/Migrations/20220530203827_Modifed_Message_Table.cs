using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatApp.Data.Migrations
{
    public partial class Modifed_Message_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SendUserId",
                table: "Messages",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SendUserId",
                table: "Messages",
                column: "SendUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_SendUserId",
                table: "Messages",
                column: "SendUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_SendUserId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_SendUserId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "SendUserId",
                table: "Messages");
        }
    }
}
