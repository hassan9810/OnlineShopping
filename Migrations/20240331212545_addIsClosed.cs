using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Migrations
{
    /// <inheritdoc />
    public partial class addIsClosed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "01ec7a84-b85f-44d2-b7ab-cc5e8adf8380");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "4a42351f-0cc5-40f4-94ac-3f5b7073959c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1eabff99-7644-424a-b221-5231c0a8e950", "AQAAAAIAAYagAAAAELpqT06Zd0yoqnBB9X4qwb+Xf6nn98YNVSQQhJNCmU2aWM/7s5CQf0t0JeBBG2jwkg==", "91083e98-3ddc-4aaa-9869-678dd3884ecc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "11ad6af3-a491-43b3-ab18-a546bc7169e1", "AQAAAAIAAYagAAAAEAWx0y8W24hfSpQRGnUjy9qxLJGPx0g3AVakoXjEFmzzk1OrRxi8rsXIs7cGY6ZS9g==", "73ee060e-9861-45ba-845e-3b9068c0e4f2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "26daf7b3-73ca-421b-bff6-87c9fc8c9a01");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "dcad3ace-f375-4abe-9e1b-ebb5098b8e7c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b323242f-6104-4fd7-8009-8db8ec5b4cec", "AQAAAAIAAYagAAAAEA9GqMlW+h6QEuJZlnStGefvFyXr17nJWlK6I9ijs7sUSjiG/MnykiqDvorXVdn6Sg==", "5286768c-10f9-46b3-a5de-33e54b60600e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "85374320-dd2c-414b-9117-1ccb228118a4", "AQAAAAIAAYagAAAAEFrYVdfY+AWF51GuTR2jUrTN0h5DMnOJdD0VvlHuOt+Nw6jxoF5K317FNMtEhOTwUQ==", "6754e993-193b-4ddc-8852-e480f296befe" });
        }
    }
}
