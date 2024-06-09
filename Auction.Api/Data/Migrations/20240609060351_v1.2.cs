using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auction.Api.Migrations
{
    /// <inheritdoc />
    public partial class v12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Items_ItemId",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Items_ItemId1",
                table: "Bids");

            migrationBuilder.DropIndex(
                name: "IX_Bids_ItemId1",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "ItemId1",
                table: "Bids");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "Bids",
                newName: "ItemFK");

            migrationBuilder.RenameIndex(
                name: "IX_Bids_ItemId",
                table: "Bids",
                newName: "IX_Bids_ItemFK");

            migrationBuilder.AddColumn<long>(
                name: "HighestBidId",
                table: "Items",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_HighestBidId",
                table: "Items",
                column: "HighestBidId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Items_ItemFK",
                table: "Bids",
                column: "ItemFK",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Bids_HighestBidId",
                table: "Items",
                column: "HighestBidId",
                principalTable: "Bids",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Items_ItemFK",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Bids_HighestBidId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_HighestBidId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "HighestBidId",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "ItemFK",
                table: "Bids",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Bids_ItemFK",
                table: "Bids",
                newName: "IX_Bids_ItemId");

            migrationBuilder.AddColumn<long>(
                name: "ItemId1",
                table: "Bids",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bids_ItemId1",
                table: "Bids",
                column: "ItemId1",
                unique: true,
                filter: "[ItemId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Items_ItemId",
                table: "Bids",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Items_ItemId1",
                table: "Bids",
                column: "ItemId1",
                principalTable: "Items",
                principalColumn: "Id");
        }
    }
}
