using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountService.Migrations
{
    /// <inheritdoc />
    public partial class ForeignnKeyApplicationTypeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_ApplicationType_ApplicationTypeAppicationId",
                table: "Account");

            migrationBuilder.RenameColumn(
                name: "ApplicationId",
                table: "Account",
                newName: "ApplicaitonTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_ApplicationType_ApplicationTypeAppicationId",
                table: "Account",
                column: "ApplicationTypeAppicationId",
                principalTable: "ApplicationType",
                principalColumn: "AppicationId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_ApplicationType_ApplicationTypeAppicationId",
                table: "Account");

            migrationBuilder.RenameColumn(
                name: "ApplicaitonTypeId",
                table: "Account",
                newName: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_ApplicationType_ApplicationTypeAppicationId",
                table: "Account",
                column: "ApplicationTypeAppicationId",
                principalTable: "ApplicationType",
                principalColumn: "AppicationId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
