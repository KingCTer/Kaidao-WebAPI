using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kaidao.Infra.CrossCutting.Identity.Migrations
{
    public partial class FixKeyAndSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_AppRoles_FunctionId",
                table: "Permissions");

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsSystemRole", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "Admin", "e1892331-9e44-40a2-8de5-c172062ee2a1", true, "Admin", "ADMIN" },
                    { "User", "f886c046-1e53-4da3-9cbd-eab6c34cc697", true, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Commands",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "APPROVE", "Duyệt" },
                    { "CREATE", "Thêm" },
                    { "DELETE", "Xoá" },
                    { "READ", "Xem" },
                    { "UPDATE", "Sửa" }
                });

            migrationBuilder.InsertData(
                table: "Functions",
                columns: new[] { "Id", "Name", "ParentId" },
                values: new object[] { "SYSTEM", "Hệ thống", null });

            migrationBuilder.InsertData(
                table: "CommandInFunctions",
                columns: new[] { "CommandId", "FunctionId", "Description" },
                values: new object[,]
                {
                    { "APPROVE", "SYSTEM", "Duyệt" },
                    { "CREATE", "SYSTEM", "Thêm" },
                    { "DELETE", "SYSTEM", "Xoá" },
                    { "READ", "SYSTEM", "Xem" },
                    { "UPDATE", "SYSTEM", "Cập nhật" }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "CommandId", "FunctionId", "RoleId" },
                values: new object[,]
                {
                    { "CREATE", "SYSTEM", "Admin" },
                    { "DELETE", "SYSTEM", "Admin" },
                    { "READ", "SYSTEM", "Admin" },
                    { "UPDATE", "SYSTEM", "Admin" },
                    { "READ", "SYSTEM", "User" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_AppRoles_RoleId",
                table: "Permissions",
                column: "RoleId",
                principalTable: "AppRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_AppRoles_RoleId",
                table: "Permissions");

            migrationBuilder.DeleteData(
                table: "CommandInFunctions",
                keyColumns: new[] { "CommandId", "FunctionId" },
                keyValues: new object[] { "APPROVE", "SYSTEM" });

            migrationBuilder.DeleteData(
                table: "CommandInFunctions",
                keyColumns: new[] { "CommandId", "FunctionId" },
                keyValues: new object[] { "CREATE", "SYSTEM" });

            migrationBuilder.DeleteData(
                table: "CommandInFunctions",
                keyColumns: new[] { "CommandId", "FunctionId" },
                keyValues: new object[] { "DELETE", "SYSTEM" });

            migrationBuilder.DeleteData(
                table: "CommandInFunctions",
                keyColumns: new[] { "CommandId", "FunctionId" },
                keyValues: new object[] { "READ", "SYSTEM" });

            migrationBuilder.DeleteData(
                table: "CommandInFunctions",
                keyColumns: new[] { "CommandId", "FunctionId" },
                keyValues: new object[] { "UPDATE", "SYSTEM" });

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumns: new[] { "CommandId", "FunctionId", "RoleId" },
                keyValues: new object[] { "CREATE", "SYSTEM", "Admin" });

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumns: new[] { "CommandId", "FunctionId", "RoleId" },
                keyValues: new object[] { "DELETE", "SYSTEM", "Admin" });

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumns: new[] { "CommandId", "FunctionId", "RoleId" },
                keyValues: new object[] { "READ", "SYSTEM", "Admin" });

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumns: new[] { "CommandId", "FunctionId", "RoleId" },
                keyValues: new object[] { "UPDATE", "SYSTEM", "Admin" });

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumns: new[] { "CommandId", "FunctionId", "RoleId" },
                keyValues: new object[] { "READ", "SYSTEM", "User" });

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: "Admin");

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: "User");

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: "APPROVE");

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: "CREATE");

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: "DELETE");

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: "READ");

            migrationBuilder.DeleteData(
                table: "Commands",
                keyColumn: "Id",
                keyValue: "UPDATE");

            migrationBuilder.DeleteData(
                table: "Functions",
                keyColumn: "Id",
                keyValue: "SYSTEM");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_AppRoles_FunctionId",
                table: "Permissions",
                column: "FunctionId",
                principalTable: "AppRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
