using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserRoleSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "public",
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.AddColumn<string>(
                name: "role_code",
                schema: "public",
                table: "roles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "user_roles",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_roles", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_roles_roles_role_id",
                        column: x => x.role_id,
                        principalSchema: "public",
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_roles_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                schema: "public",
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "role_code",
                value: "_ADMIN_");

            migrationBuilder.UpdateData(
                schema: "public",
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "role_code",
                value: "_SUPPORT_");

            migrationBuilder.UpdateData(
                schema: "public",
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                column: "role_code",
                value: "_ANALYTICS_");

            migrationBuilder.UpdateData(
                schema: "public",
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                column: "role_code",
                value: "_PAYMENT_ADMIN_");

            migrationBuilder.UpdateData(
                schema: "public",
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                columns: new[] { "description", "role_code", "role_name" },
                values: new object[] { "Common/Normal User", "_PUBLIC_USER_", "Common Usser" });

            migrationBuilder.InsertData(
                schema: "public",
                table: "users",
                columns: new[] { "id", "created_at", "email", "full_name", "is_email_verified", "is_mfa_enabled", "password_hash", "phone", "status", "updated_at" },
                values: new object[] { new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), new DateTime(2025, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc), "admin@auth.dapplesoft.com", "Default Admin", false, false, "60358AD3245A0E1D8FC2CA0B0914E45C5F87143DDB2C9E81E09B4E41676F30B8-99D093AF2C44DB8DDCA9FE77BDE4A9F2", null, 0, new DateTime(2025, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                schema: "public",
                table: "user_roles",
                columns: new[] { "id", "role_id", "user_id" },
                values: new object[] { new Guid("aaaaaaaa-eeee-ffff-ffff-ffffffffffff"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff") });

            migrationBuilder.CreateIndex(
                name: "ix_user_roles_role_id",
                schema: "public",
                table: "user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_roles_user_id",
                schema: "public",
                table: "user_roles",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_roles",
                schema: "public");

            migrationBuilder.DeleteData(
                schema: "public",
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"));

            migrationBuilder.DropColumn(
                name: "role_code",
                schema: "public",
                table: "roles");

            migrationBuilder.UpdateData(
                schema: "public",
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                columns: new[] { "description", "role_name" },
                values: new object[] { "Common User", "Client" });

            migrationBuilder.InsertData(
                schema: "public",
                table: "users",
                columns: new[] { "id", "created_at", "email", "full_name", "is_email_verified", "is_mfa_enabled", "password_hash", "phone", "status", "updated_at" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc), "user1@gmail.com", "System Admin", false, false, "0CB47CF84CA0824A48EB7CDAD0B13AC83D6742E85A21B8A0FF58A235C2050DE9-ED1FD94795D453D2320B0A5444D4B31E", null, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc), "user2@gmail.com", "Normal User", false, false, "CDFCF4E8D89841B7A49EC50581EC9F5CA3AB0A93A9F23B78C69839B18BE43752-C4F0917170B9972DDE5015CBCFE31786", null, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc), "user3@gmail.com", "Demo User", false, false, "D3A38C51393060353567AF0865FC91B4E435AB433D177AF056F79BA1AEEADA0B-852250D8F97163710CF73F51EF6EE70D", null, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }
    }
}
