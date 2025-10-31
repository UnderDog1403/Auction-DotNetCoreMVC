using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Payments_AuctionID",
                table: "Payments");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_AuctionID",
                table: "Payments",
                column: "AuctionID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Payments_AuctionID",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Payments");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_AuctionID",
                table: "Payments",
                column: "AuctionID");
        }
    }
}
