using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeMG.Data.Migrations
{
    public partial class _23s2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    RegistrationNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "None"),
                    CompanyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    WebSite = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(900)", maxLength: 900, nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.RegistrationNumber);
                });

            migrationBuilder.CreateTable(
                name: "Cost",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, defaultValue: 1L),
                    Forthe = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CostDate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cost", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DepositSalary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonnelCode = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Month = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositSalary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    Code = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    FoodName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "FoodOffered",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    OfferedDate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Day = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Meal = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Food = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodOffered", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Goods",
                columns: table => new
                {
                    Code = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "LendingGoods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    PersonnelCode = table.Column<int>(type: "int", nullable: false),
                    GoodsCode = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LendingGoods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReservationFood",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    PersonnelCode = table.Column<int>(type: "int", nullable: false),
                    OfferedCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationFood", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    VacationDay = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shift",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    PersonnelCode = table.Column<int>(type: "int", nullable: false),
                    ShiftDate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShiftDay = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShiftTime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shift", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TIo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    PersonnelCode = table.Column<int>(type: "int", nullable: false),
                    DateOfDay = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EntranceTime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ExitTime = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    Code = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Vacation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    PersonnelCode = table.Column<int>(type: "int", nullable: false),
                    FromDate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    inmonth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountDate = table.Column<int>(type: "int", nullable: false),
                    PersonnelCodeSuccessor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AmountOfSalary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    RightHousing = table.Column<int>(type: "int", nullable: false),
                    RightChild = table.Column<int>(type: "int", nullable: false),
                    Insurance = table.Column<int>(type: "int", nullable: false),
                    HourOfOverTime = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmountOfSalary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AmountOfSalary_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    PersonnelCode = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    CodeMelli = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfEmployment = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DegreeEducation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaritalStatus = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Place = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(900)", maxLength: 900, nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unitid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.PersonnelCode);
                    table.ForeignKey(
                        name: "FK_Employee_Unit_Unitid",
                        column: x => x.Unitid,
                        principalTable: "Unit",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BankAccount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Shaba = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PersonnelCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccount_Employee_PersonnelCode",
                        column: x => x.PersonnelCode,
                        principalTable: "Employee",
                        principalColumn: "PersonnelCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NUTritionCard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Balance = table.Column<int>(type: "int", nullable: false),
                    PersonnelCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NUTritionCard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NUTritionCard_Employee_PersonnelCode",
                        column: x => x.PersonnelCode,
                        principalTable: "Employee",
                        principalColumn: "PersonnelCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AmountOfSalary_UnitId",
                table: "AmountOfSalary",
                column: "UnitId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_PersonnelCode",
                table: "BankAccount",
                column: "PersonnelCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Unitid",
                table: "Employee",
                column: "Unitid");

            migrationBuilder.CreateIndex(
                name: "IX_NUTritionCard_PersonnelCode",
                table: "NUTritionCard",
                column: "PersonnelCode",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmountOfSalary");

            migrationBuilder.DropTable(
                name: "BankAccount");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Cost");

            migrationBuilder.DropTable(
                name: "DepositSalary");

            migrationBuilder.DropTable(
                name: "Food");

            migrationBuilder.DropTable(
                name: "FoodOffered");

            migrationBuilder.DropTable(
                name: "Goods");

            migrationBuilder.DropTable(
                name: "LendingGoods");

            migrationBuilder.DropTable(
                name: "NUTritionCard");

            migrationBuilder.DropTable(
                name: "ReservationFood");

            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "Shift");

            migrationBuilder.DropTable(
                name: "TIo");

            migrationBuilder.DropTable(
                name: "Vacation");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Unit");
        }
    }
}
