using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC.Migrations
{
    /// <inheritdoc />
    public partial class updatetoguid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Xóa khóa ngoại giữa Orders và Customers
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            // 2. Xóa chỉ mục phụ thuộc vào cột CustomerId trong Orders
            migrationBuilder.DropIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders");

            // 3. Xóa khóa chính của cột CustomerId trong Customers
            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            // 4. Thêm cột GUID mới cho CustomerId trong Customers và Orders
            migrationBuilder.AddColumn<Guid>(
                name: "NewCustomerId",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.NewGuid());

            migrationBuilder.AddColumn<Guid>(
                name: "NewCustomerId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.NewGuid());

            // 5. Sao chép dữ liệu từ CustomerId cũ sang NewCustomerId
            migrationBuilder.Sql("UPDATE Customers SET NewCustomerId = NEWID()");
            migrationBuilder.Sql("UPDATE Orders SET NewCustomerId = NEWID()");

            // 6. Xóa cột CustomerId cũ
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Orders");

            // 7. Đổi tên cột NewCustomerId thành CustomerId
            migrationBuilder.RenameColumn(
                name: "NewCustomerId",
                table: "Customers",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "NewCustomerId",
                table: "Orders",
                newName: "CustomerId");

            // 8. Thêm lại khóa chính mới cho cột CustomerId trong Customers
            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "CustomerId");

            // 9. Thêm lại khóa ngoại mới cho cột CustomerId trong Orders
            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            // 10. Thêm lại chỉ mục phụ thuộc vào cột CustomerId trong Orders
            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId"); ;
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // 1. Xóa khóa ngoại giữa Orders và Customers
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            // 2. Xóa khóa chính của cột CustomerId trong Customers
            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            // 3. Xóa cột CustomerId mới
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Orders");

            // 4. Thêm lại cột CustomerId kiểu int trong Customers và Orders
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // 5. Thêm lại khóa chính cũ cho cột CustomerId trong Customers
            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "CustomerId");

            // 6. Thêm lại khóa ngoại cũ giữa Orders và Customers
            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            // 7. Thêm lại chỉ mục phụ thuộc vào cột CustomerId trong Orders
            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");
        }
    }
}
