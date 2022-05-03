using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject.Migrations
{
    public partial class Bills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HowMany",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bills_ItemId",
                table: "Bills",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Items_ItemId",
                table: "Bills",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Items_ItemId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_ItemId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "HowMany",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Bills");
        }
    }
}
