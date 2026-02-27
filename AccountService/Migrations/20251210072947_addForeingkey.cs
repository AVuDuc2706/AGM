using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountService.Migrations
{
    /// <inheritdoc />
    public partial class addForeingkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationTypeId",
                table: "Account",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Account_ApplicationTypeId",
                table: "Account",
                column: "ApplicationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_ApplicationType_ApplicationTypeId",
                table: "Account",
                column: "ApplicationTypeId",
                principalTable: "ApplicationType",
                principalColumn: "ApplicationId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_ApplicationType_ApplicationTypeId",
                table: "Account");

            migrationBuilder.DropIndex(
                name: "IX_Account_ApplicationTypeId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "ApplicationTypeId",
                table: "Account");
        }
    }
}
