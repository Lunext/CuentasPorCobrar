using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddingSomethings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_AccountingEntries_AccountingEntryId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_AccountingEntryId",
                table: "Transactions");

            migrationBuilder.AlterColumn<string>(
                name: "AccountingEntryId",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "id_EC",
                table: "AccountingEntries",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "id_EC",
                table: "AccountingEntries");

            migrationBuilder.AlterColumn<int>(
                name: "AccountingEntryId",
                table: "Transactions",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountingEntryId",
                table: "Transactions",
                column: "AccountingEntryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_AccountingEntries_AccountingEntryId",
                table: "Transactions",
                column: "AccountingEntryId",
                principalTable: "AccountingEntries",
                principalColumn: "AccountingEntryId");
        }
    }
}
