using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oneonones.Repositories.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "meeting",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    leader_id = table.Column<Guid>(type: "uuid", nullable: false),
                    led_id = table.Column<Guid>(type: "uuid", nullable: false),
                    meeting_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    annotation = table.Column<string>(type: "character varying(2047)", maxLength: 2047, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meeting", x => x.id);
                    table.ForeignKey(
                        name: "FK_meeting_employee_leader_id",
                        column: x => x.leader_id,
                        principalTable: "employee",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_meeting_employee_led_id",
                        column: x => x.led_id,
                        principalTable: "employee",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "oneonone",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    leader_id = table.Column<Guid>(type: "uuid", nullable: false),
                    led_id = table.Column<Guid>(type: "uuid", nullable: false),
                    frequency_type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oneonone", x => x.id);
                    table.ForeignKey(
                        name: "FK_oneonone_employee_leader_id",
                        column: x => x.leader_id,
                        principalTable: "employee",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_oneonone_employee_led_id",
                        column: x => x.led_id,
                        principalTable: "employee",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_employee_email",
                table: "employee",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_meeting_leader",
                table: "meeting",
                columns: new[] { "leader_id", "led_id", "meeting_date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_meeting_led",
                table: "meeting",
                columns: new[] { "led_id", "leader_id", "meeting_date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_oneonone_leader",
                table: "oneonone",
                columns: new[] { "leader_id", "led_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_oneonone_led",
                table: "oneonone",
                columns: new[] { "led_id", "leader_id" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "meeting");

            migrationBuilder.DropTable(
                name: "oneonone");

            migrationBuilder.DropTable(
                name: "employee");
        }
    }
}
