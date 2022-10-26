using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class migration_fix_names : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAEAAAPoAAAAELTBzp20JCqqXurxJxk9AQArgmiKjHLnkPT5cgxh9JFqYgcxbP9LCCEOw+s+HD3M0Q==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAEAAAPoAAAAEPAMF4fHijEKuvlGANCF8KCWD2l+Qn3Il6d6/DjayHADFXgc+IQADl0Ino5pe8Z03w==");
        }
    }
}
