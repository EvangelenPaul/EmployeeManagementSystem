using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorPagesTurorial.Services.Migrations
{
    public partial class spGetEmployeeById : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"create procedure spGetEmployeeById
                                    @Id int
                                        as
                                    Begin
	                                Select * from Employee
	                                where Id = @Id
                                    End";

            migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Drop procedure spGetEmployeeById";
            migrationBuilder.Sql(procedure);
        }
    }
}
