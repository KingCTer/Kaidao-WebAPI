using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaidao.Infra.CrossCutting.Identity.Migrations
{
    public partial class FixForenKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissions_AppUsers_FunctionId",
                table: "UserPermissions");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: "Admin",
                column: "ConcurrencyStamp",
                value: "afaa50cb-4f07-4718-b788-cdf7716cffc8");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: "User",
                column: "ConcurrencyStamp",
                value: "a1d22c46-a65a-4977-9903-d6e56315f60e");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissions_AppUsers_UserId",
                table: "UserPermissions",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissions_AppUsers_UserId",
                table: "UserPermissions");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: "Admin",
                column: "ConcurrencyStamp",
                value: "e1892331-9e44-40a2-8de5-c172062ee2a1");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: "User",
                column: "ConcurrencyStamp",
                value: "f886c046-1e53-4da3-9cbd-eab6c34cc697");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissions_AppUsers_FunctionId",
                table: "UserPermissions",
                column: "FunctionId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
