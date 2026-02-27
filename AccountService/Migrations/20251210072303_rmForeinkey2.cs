using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountService.Migrations
{
    /// <inheritdoc />
    public partial class rmForeinkey2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_ApplicationType_ApplicationTypeAppicationId",
                table: "Account");

            migrationBuilder.DropIndex(
                name: "IX_Account_ApplicationTypeAppicationId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "ApplicationTypeAppicationId",
                table: "Account");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationTypeAppicationId",
                table: "Account",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Account_ApplicationTypeAppicationId",
                table: "Account",
                column: "ApplicationTypeAppicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_ApplicationType_ApplicationTypeAppicationId",
                table: "Account",
                column: "ApplicationTypeAppicationId",
                principalTable: "ApplicationType",
                principalColumn: "AppicationId");
        }
    }
}
