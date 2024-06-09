using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auction.Api.Migrations
{
    /// <inheritdoc />
    public partial class v11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HighestBidId",
                table: "Items");

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
                name: "FK_Bids_Items_ItemId1",
                table: "Bids",
                column: "ItemId1",
                principalTable: "Items",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Items_ItemId1",
                table: "Bids");

            migrationBuilder.DropIndex(
                name: "IX_Bids_ItemId1",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "ItemId1",
                table: "Bids");

            migrationBuilder.AddColumn<long>(
                name: "HighestBidId",
                table: "Items",
                type: "bigint",
                nullable: true);
        }
    }
}
