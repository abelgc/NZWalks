using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingdataforRegionsandDifficulties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("140e9582-b3e8-4e62-a465-62cc93c7a693"), "Easy" },
                    { new Guid("57de06a4-c559-4c17-84d7-cb09ffcc005e"), "Hard" },
                    { new Guid("c5882d53-51ce-4012-a412-0f2b2ad58813"), "Medium" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionalImageUrl" },
                values: new object[,]
                {
                    { new Guid("14ceba71-4b51-4777-9b17-46602cf66153"), "Centre", "Granollers", "https://www.thetravelpocketguide.com/wp-content/uploads/FEATURE_Places_Catalonia-759x500.jpg" },
                    { new Guid("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"), "SUR", "Tarragona", "https://cdn.britannica.com/14/144714-050-D1641493/Roman-amphitheatre-Tarragona-Spain.jpg?w=300" },
                    { new Guid("906cb139-415a-4bbb-a174-1a1faf9fb1f6"), "SUR", "Torredembarra", null },
                    { new Guid("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"), "Interior", "La Garriga", "https://www.tripadvisor.com/Hotel_Feature-g488306-d482317-zft1-Hotel_Termes_La_Garriga.html" },
                    { new Guid("f077a22e-4248-4bf6-b564-c7cf4e250263"), "East", "Terrassa", null },
                    { new Guid("f7248fc3-2585-4efb-8d1d-1c555f4087f6"), "SUR", "Reus", "http://www.catalonia-valencia.com/wp-content/uploads/2014/08/Reus-Travel-Guide.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("140e9582-b3e8-4e62-a465-62cc93c7a693"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("57de06a4-c559-4c17-84d7-cb09ffcc005e"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("c5882d53-51ce-4012-a412-0f2b2ad58813"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("14ceba71-4b51-4777-9b17-46602cf66153"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("906cb139-415a-4bbb-a174-1a1faf9fb1f6"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f077a22e-4248-4bf6-b564-c7cf4e250263"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f7248fc3-2585-4efb-8d1d-1c555f4087f6"));
        }
    }
}
