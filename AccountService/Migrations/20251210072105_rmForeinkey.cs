using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountService.Migrations
{
    /// <inheritdoc />
    public partial class rmForeinkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_ApplicationType_ApplicaitonTypeId",
                table: "Account");

            migrationBuilder.DropIndex(
                name: "IX_Account_ApplicaitonTypeId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "ApplicaitonTypeId",
                table: "Account");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicaitonTypeId",
                table: "Account",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Account_ApplicaitonTypeId",
                table: "Account",
                column: "ApplicaitonTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_ApplicationType_ApplicaitonTypeId",
                table: "Account",
                column: "ApplicaitonTypeId",
                principalTable: "ApplicationType",
                principalColumn: "AppicationId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
