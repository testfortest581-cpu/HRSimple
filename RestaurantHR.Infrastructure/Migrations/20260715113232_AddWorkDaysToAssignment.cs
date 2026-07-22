using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantHR.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkDaysToAssignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WorkDays",
                table: "EmployeeAssignments",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkDays",
                table: "EmployeeAssignments");
        }
    }
}
