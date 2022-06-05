using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatApp.Data.Migrations
{
    public partial class Cascade_Delete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_SendUserId",
                table: "Messages");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_SendUserId",
                table: "Messages",
                column: "SendUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_SendUserId",
                table: "Messages");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_SendUserId",
                table: "Messages",
                column: "SendUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
