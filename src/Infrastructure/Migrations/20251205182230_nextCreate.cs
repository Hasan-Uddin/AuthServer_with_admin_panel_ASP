using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class nextCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "otp",
                schema: "public",
                columns: table => new
                {
                    otp_id = table.Column<Guid>(type: "uuid", nullable: false),
                    otp_token = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    delay = table.Column<TimeSpan>(type: "interval", nullable: false),
                    is_expired = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_otp", x => x.otp_id);
                });

            migrationBuilder.CreateTable(
                name: "smtp_config",
                schema: "public",
                columns: table => new
                {
                    smtp_id = table.Column<Guid>(type: "uuid", nullable: false),
                    host = table.Column<string>(type: "text", nullable: false),
                    port = table.Column<int>(type: "integer", nullable: false),
                    username = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    password = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    enable_ssl = table.Column<bool>(type: "boolean", nullable: false),
                    sender_email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_smtp_config", x => x.smtp_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "otp",
                schema: "public");

            migrationBuilder.DropTable(
                name: "smtp_config",
                schema: "public");
        }
    }
}
