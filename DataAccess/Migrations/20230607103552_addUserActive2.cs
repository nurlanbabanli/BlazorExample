using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addUserActive2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserOperations",
                table: "UserOperations");

            migrationBuilder.RenameTable(
                name: "UserOperations",
                newName: "UserOperationClaims");

            migrationBuilder.RenameIndex(
                name: "IX_UserOperations_Id",
                table: "UserOperationClaims",
                newName: "IX_UserOperationClaims_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserOperationClaims",
                table: "UserOperationClaims",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserOperationClaims",
                table: "UserOperationClaims");

            migrationBuilder.RenameTable(
                name: "UserOperationClaims",
                newName: "UserOperations");

            migrationBuilder.RenameIndex(
                name: "IX_UserOperationClaims_Id",
                table: "UserOperations",
                newName: "IX_UserOperations_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserOperations",
                table: "UserOperations",
                column: "Id");
        }
    }
}
