using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp.Migrations
{
    /// <inheritdoc />
    public partial class AddBidTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bid_Auctions_AuctionID",
                table: "Bid");

            migrationBuilder.DropForeignKey(
                name: "FK_Bid_Users_UserID",
                table: "Bid");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bid",
                table: "Bid");

            migrationBuilder.RenameTable(
                name: "Bid",
                newName: "Bids");

            migrationBuilder.RenameIndex(
                name: "IX_Bid_UserID",
                table: "Bids",
                newName: "IX_Bids_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Bid_AuctionID",
                table: "Bids",
                newName: "IX_Bids_AuctionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bids",
                table: "Bids",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Auctions_AuctionID",
                table: "Bids",
                column: "AuctionID",
                principalTable: "Auctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Users_UserID",
                table: "Bids",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Auctions_AuctionID",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Users_UserID",
                table: "Bids");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bids",
                table: "Bids");

            migrationBuilder.RenameTable(
                name: "Bids",
                newName: "Bid");

            migrationBuilder.RenameIndex(
                name: "IX_Bids_UserID",
                table: "Bid",
                newName: "IX_Bid_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Bids_AuctionID",
                table: "Bid",
                newName: "IX_Bid_AuctionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bid",
                table: "Bid",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bid_Auctions_AuctionID",
                table: "Bid",
                column: "AuctionID",
                principalTable: "Auctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bid_Users_UserID",
                table: "Bid",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
