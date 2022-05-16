using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject.Migrations
{
    public partial class FK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillDetail_Bills_BillId",
                table: "BillDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_BillDetail_Items_ItemId",
                table: "BillDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BillDetail",
                table: "BillDetail");

            migrationBuilder.RenameTable(
                name: "BillDetail",
                newName: "BillDetails");

            migrationBuilder.RenameIndex(
                name: "IX_BillDetail_ItemId",
                table: "BillDetails",
                newName: "IX_BillDetails_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_BillDetail_BillId",
                table: "BillDetails",
                newName: "IX_BillDetails_BillId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillDetails",
                table: "BillDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BillDetails_Bills_BillId",
                table: "BillDetails",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillDetails_Items_ItemId",
                table: "BillDetails",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillDetails_Bills_BillId",
                table: "BillDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_BillDetails_Items_ItemId",
                table: "BillDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BillDetails",
                table: "BillDetails");

            migrationBuilder.RenameTable(
                name: "BillDetails",
                newName: "BillDetail");

            migrationBuilder.RenameIndex(
                name: "IX_BillDetails_ItemId",
                table: "BillDetail",
                newName: "IX_BillDetail_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_BillDetails_BillId",
                table: "BillDetail",
                newName: "IX_BillDetail_BillId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillDetail",
                table: "BillDetail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BillDetail_Bills_BillId",
                table: "BillDetail",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillDetail_Items_ItemId",
                table: "BillDetail",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
