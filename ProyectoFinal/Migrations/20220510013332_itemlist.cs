using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject.Migrations
{
    public partial class itemlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "BillDetailId",
                table: "Bills",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BillDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    BillId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillDetail_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BillDetail_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_BillDetailId",
                table: "Bills",
                column: "BillDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetail_BillId",
                table: "BillDetail",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetail_ItemId",
                table: "BillDetail",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_BillDetail_BillDetailId",
                table: "Bills",
                column: "BillDetailId",
                principalTable: "BillDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_BillDetail_BillDetailId",
                table: "Bills");

            migrationBuilder.DropTable(
                name: "BillDetail");

            migrationBuilder.DropIndex(
                name: "IX_Bills_BillDetailId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "BillDetailId",
                table: "Bills");

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
    }
}
