using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace MineDev.MineTrack.Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                    name: "users",
                    columns: table => new
                    {
                        id = table.Column<int>(type: "int", nullable: false)
                            .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                        username = table.Column<string>(type: "longtext", nullable: false),
                        email = table.Column<string>(type: "longtext", nullable: false),
                        full_name = table.Column<string>(type: "longtext", nullable: false),
                        phone = table.Column<string>(type: "longtext", nullable: false),
                        company = table.Column<string>(type: "longtext", nullable: false),
                        role = table.Column<string>(type: "longtext", nullable: false),
                        password_hash = table.Column<string>(type: "longtext", nullable: false),
                        created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                        updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("p_k_users", x => x.id);
                    })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "users");
        }
    }
}
