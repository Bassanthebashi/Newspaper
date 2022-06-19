using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Neureveal.Migrations
{
    public partial class initial4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Content", "CreatedDate", "IsActive", "Title" },
                values: new object[,]
                {
                    { new Guid("695848c6-f271-4d40-957f-efef0244950e"), "Pure OOP", new DateTime(2022, 6, 17, 14, 12, 7, 748, DateTimeKind.Local).AddTicks(1006), true, "C#" },
                    { new Guid("c1cc93b2-0deb-4e3c-ac11-e9d147266820"), "Minerals and spinal", new DateTime(2022, 6, 17, 14, 12, 7, 748, DateTimeKind.Local).AddTicks(979), true, "Bones composition" },
                    { new Guid("c91be404-ef2f-4ce2-9ad3-d73265fbe0ce"), "Not Pure OOP", new DateTime(2022, 6, 17, 14, 12, 7, 748, DateTimeKind.Local).AddTicks(1004), true, "C++" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3a1e0094-5d91-4989-a03d-b2966f9d5a84"), "MentalHealth" },
                    { new Guid("81a62810-734a-4904-a1e9-9b7724eeddfa"), "Programming" },
                    { new Guid("961fa211-656b-44d2-bff2-e34f2252b409"), "Science" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("695848c6-f271-4d40-957f-efef0244950e"));

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("c1cc93b2-0deb-4e3c-ac11-e9d147266820"));

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: new Guid("c91be404-ef2f-4ce2-9ad3-d73265fbe0ce"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("3a1e0094-5d91-4989-a03d-b2966f9d5a84"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("81a62810-734a-4904-a1e9-9b7724eeddfa"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("961fa211-656b-44d2-bff2-e34f2252b409"));
        }
    }
}
