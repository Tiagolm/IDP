using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class migration_fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhoneContacts_PhoneContactTypes_TipoContatoTelefoneId",
                table: "PhoneContacts");

            migrationBuilder.RenameColumn(
                name: "TipoContatoTelefoneId",
                table: "PhoneContacts",
                newName: "PhoneContactTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneContacts_TipoContatoTelefoneId",
                table: "PhoneContacts",
                newName: "IX_PhoneContacts_PhoneContactTypeId");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Contacts",
                newName: "Name");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAEAAAPoAAAAEPAMF4fHijEKuvlGANCF8KCWD2l+Qn3Il6d6/DjayHADFXgc+IQADl0Ino5pe8Z03w==");

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneContacts_PhoneContactTypes_PhoneContactTypeId",
                table: "PhoneContacts",
                column: "PhoneContactTypeId",
                principalTable: "PhoneContactTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhoneContacts_PhoneContactTypes_PhoneContactTypeId",
                table: "PhoneContacts");

            migrationBuilder.RenameColumn(
                name: "PhoneContactTypeId",
                table: "PhoneContacts",
                newName: "TipoContatoTelefoneId");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneContacts_PhoneContactTypeId",
                table: "PhoneContacts",
                newName: "IX_PhoneContacts_TipoContatoTelefoneId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Contacts",
                newName: "Nome");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAEAAAPoAAAAEHz/KozFNB/J5qq6ND0p54DK3bWWNavo4PQDaaQrVOP2vrXODbnsOreHzYWTXi7XOQ==");

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneContacts_PhoneContactTypes_TipoContatoTelefoneId",
                table: "PhoneContacts",
                column: "TipoContatoTelefoneId",
                principalTable: "PhoneContactTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}