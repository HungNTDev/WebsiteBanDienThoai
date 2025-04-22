using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class data_PaymentType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PaymentTypes",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDate", "IsActive", "UpdatedBy", "UpdatedDate", "Value" },
                values: new object[,]
                {
                    { new Guid("7e47cc15-8dcd-4b46-a559-4ff25642d8d0"), "CASH", null, new DateTime(2025, 4, 20, 19, 13, 31, 307, DateTimeKind.Local).AddTicks(2682), true, null, new DateTime(2025, 4, 20, 19, 13, 31, 307, DateTimeKind.Local).AddTicks(2691), "Thanh toán tại cửa hàng" },
                    { new Guid("c5900418-2763-40ce-a618-b5afded03392"), "VNPAY", null, new DateTime(2025, 4, 20, 19, 13, 31, 307, DateTimeKind.Local).AddTicks(2695), true, null, new DateTime(2025, 4, 20, 19, 13, 31, 307, DateTimeKind.Local).AddTicks(2695), "Thanh toán qua VNPAY" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: new Guid("7e47cc15-8dcd-4b46-a559-4ff25642d8d0"));

            migrationBuilder.DeleteData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: new Guid("c5900418-2763-40ce-a618-b5afded03392"));
        }
    }
}
