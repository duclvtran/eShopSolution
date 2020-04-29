using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class addImageManagement_apis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2020, 4, 28, 21, 53, 43, 784, DateTimeKind.Local).AddTicks(7241),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 4, 28, 0, 14, 43, 132, DateTimeKind.Local).AddTicks(9977));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "0312b88f-5ec4-4bc7-925b-a8d8e5615c13");

            migrationBuilder.UpdateData(
                table: "AppUses",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ef921a00-d71a-40d8-90fd-2b4bfbbf8635", "AQAAAAEAACcQAAAAEPZhDcy2xHje+HraO/aOkuEGMGe8xoEVwnlLmrJoWBQd19rqHRiqyR69KyQiuzMWrg==" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 4, 28, 21, 53, 43, 808, DateTimeKind.Local).AddTicks(6227));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 4, 28, 0, 14, 43, 132, DateTimeKind.Local).AddTicks(9977),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 4, 28, 21, 53, 43, 784, DateTimeKind.Local).AddTicks(7241));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "f4cba905-ce1f-4fcd-93a8-f504a1ba23ff");

            migrationBuilder.UpdateData(
                table: "AppUses",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d2c4e122-62e5-420f-9fa3-d77e708cd5f4", "AQAAAAEAACcQAAAAEPdErTozAlt02uqRUJHi7zR4VsSfk94AWE9Dgi14an7d3ikxIp6e8L69y/MFCkwKEQ==" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 4, 28, 0, 14, 43, 164, DateTimeKind.Local).AddTicks(5349));
        }
    }
}
