using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolMealSubscription.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class handleOrderReation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Grades_GradeId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_GradeId",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "Grade Id",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "de377bbe-0d39-45b1-8ab5-69960a883a2a", "AQAAAAIAAYagAAAAEIRZJJDogAe5yakO48SN/iyHyhIsfpVmuCPwnVEJhqho+hW+Skbd5UxskQ5p/IXPTg==", "51374737-a6f3-44e2-a71f-872393ea2dfc" });

            migrationBuilder.CreateIndex(
                name: "IX_Students_Grade Id",
                table: "Students",
                column: "Grade Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ApplicationUserId",
                table: "Orders",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_ApplicationUserId",
                table: "Orders",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Grades_Grade Id",
                table: "Students",
                column: "Grade Id",
                principalTable: "Grades",
                principalColumn: "GradeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_ApplicationUserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Grades_Grade Id",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_Grade Id",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ApplicationUserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Grade Id",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ce3948d2-135e-4f1a-8900-54ffd545d8ab", "AQAAAAIAAYagAAAAEDH2fekP2u55tpXMfe5H4/EyvtwgjEhAEnkmLADvuWHF6kmafOOMLKZNeI07Y2oIww==", "035c7553-8b8f-4df1-bc04-73e891ad4d77" });

            migrationBuilder.CreateIndex(
                name: "IX_Students_GradeId",
                table: "Students",
                column: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Grades_GradeId",
                table: "Students",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "GradeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
