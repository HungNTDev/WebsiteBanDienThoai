using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class add_field_Code_Table_Order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: new Guid("7e47cc15-8dcd-4b46-a559-4ff25642d8d0"));

            migrationBuilder.DeleteData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: new Guid("c5900418-2763-40ce-a618-b5afded03392"));

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "PaymentTypes",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "IsActive", "UpdatedBy", "UpdatedDate", "Value" },
                values: new object[,]
                {
                    { new Guid("899e183e-fd08-42b0-abc2-3339bc4b479c"), "VNPAY", null, new DateTime(2025, 4, 23, 11, 49, 18, 828, DateTimeKind.Local).AddTicks(7583), true, null, new DateTime(2025, 4, 23, 11, 49, 18, 828, DateTimeKind.Local).AddTicks(7583), "Thanh toán qua VNPAY" },
                    { new Guid("ea7d4939-1c79-4004-9a1a-3a85f2336581"), "CASH", null, new DateTime(2025, 4, 23, 11, 49, 18, 828, DateTimeKind.Local).AddTicks(7564), true, null, new DateTime(2025, 4, 23, 11, 49, 18, 828, DateTimeKind.Local).AddTicks(7574), "Thanh toán tại cửa hàng" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: new Guid("899e183e-fd08-42b0-abc2-3339bc4b479c"));

            migrationBuilder.DeleteData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: new Guid("ea7d4939-1c79-4004-9a1a-3a85f2336581"));

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "PaymentTypes",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "IsActive", "UpdatedBy", "UpdatedDate", "Value" },
                values: new object[,]
                {
                    { new Guid("7e47cc15-8dcd-4b46-a559-4ff25642d8d0"), "CASH", null, new DateTime(2025, 4, 20, 19, 13, 31, 307, DateTimeKind.Local).AddTicks(2682), true, null, new DateTime(2025, 4, 20, 19, 13, 31, 307, DateTimeKind.Local).AddTicks(2691), "Thanh toán tại cửa hàng" },
                    { new Guid("c5900418-2763-40ce-a618-b5afded03392"), "VNPAY", null, new DateTime(2025, 4, 20, 19, 13, 31, 307, DateTimeKind.Local).AddTicks(2695), true, null, new DateTime(2025, 4, 20, 19, 13, 31, 307, DateTimeKind.Local).AddTicks(2695), "Thanh toán qua VNPAY" }
                });
        }
    }
}
