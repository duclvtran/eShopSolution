using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class AddLogin_Jwt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2020, 4, 29, 15, 3, 6, 905, DateTimeKind.Local).AddTicks(8268),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 4, 28, 21, 53, 43, 784, DateTimeKind.Local).AddTicks(7241));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "4f097663-636d-444b-b075-8ef00179bbbd");

            migrationBuilder.UpdateData(
                table: "AppUses",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "21c7b740-0e49-4a5b-847b-a8c809998198", "AQAAAAEAACcQAAAAEAzghEJBo71WprNrQjwkKNVqiq+ptOQqa8ufG0Nr11zA5zFWKyMB22tLHcG6iGlZqQ==" });

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
                value: new DateTime(2020, 4, 29, 15, 3, 6, 929, DateTimeKind.Local).AddTicks(4788));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 4, 28, 21, 53, 43, 784, DateTimeKind.Local).AddTicks(7241),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 4, 29, 15, 3, 6, 905, DateTimeKind.Local).AddTicks(8268));

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
    }
}
