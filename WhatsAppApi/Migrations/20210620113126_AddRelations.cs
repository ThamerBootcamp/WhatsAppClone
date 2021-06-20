using Microsoft.EntityFrameworkCore.Migrations;

namespace WhatsAppApi.Migrations
{
    public partial class AddRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GuestId",
                table: "Rooms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Rooms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Room_ID",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_GuestId",
                table: "Rooms",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_OwnerId",
                table: "Rooms",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RoomId",
                table: "Messages",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Rooms_RoomId",
                table: "Messages",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Users_GuestId",
                table: "Rooms",
                column: "GuestId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Users_OwnerId",
                table: "Rooms",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Rooms_RoomId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Users_GuestId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Users_OwnerId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_GuestId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_OwnerId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Messages_RoomId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "GuestId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Room_ID",
                table: "Messages");
        }
    }
}
