﻿// <auto-generated />
using EmployeeMG.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmployeeMG.Data.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("EmployeeMG.Data.Entities.TAmountOfSalary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<int>("HourOfOverTime")
                        .HasColumnType("int");

                    b.Property<int>("Insurance")
                        .HasColumnType("int");

                    b.Property<int>("RightChild")
                        .HasColumnType("int");

                    b.Property<int>("RightHousing")
                        .HasColumnType("int");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.Property<int>("UnitId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UnitId")
                        .IsUnique();

                    b.ToTable("AmountOfSalary");
                });

            modelBuilder.Entity("EmployeeMG.Data.Entities.TBankAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("PersonnelCode")
                        .HasColumnType("int");

                    b.Property<string>("Shaba")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("PersonnelCode")
                        .IsUnique();

                    b.ToTable("BankAccount");
                });

            modelBuilder.Entity("EmployeeMG.Data.Entities.TCompany", b =>
                {
                    b.Property<string>("RegistrationNumber")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValue("None");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(900)
                        .HasColumnType("nvarchar(900)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("WebSite")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("RegistrationNumber");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("EmployeeMG.Data.Entities.TCost", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValue(1L);

                    b.Property<long>("Amount")
                        .HasColumnType("bigint");

                    b.Property<string>("CostDate")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Forthe")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Cost");
                });

            modelBuilder.Entity("EmployeeMG.Data.Entities.TDepositSalary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<long>("Amount")
                        .HasColumnType("bigint");

                    b.Property<string>("Month")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("PersonnelCode")
                        .HasColumnType("int");

                    b.Property<string>("Year")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("DepositSalary");
                });

            modelBuilder.Entity("EmployeeMG.Data.Entities.TEmployee", b =>
                {
                    b.Property<int>("PersonnelCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(900)
                        .HasColumnType("nvarchar(900)");

                    b.Property<string>("CodeMelli")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("DateOfBirth")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("DateOfEmployment")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("DegreeEducation")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FatherName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("MaritalStatus")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Picture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Place")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<int>("Unitid")
                        .HasColumnType("int");

                    b.HasKey("PersonnelCode");

                    b.HasIndex("Unitid");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("EmployeeMG.Data.Entities.TFood", b =>
                {
                    b.Property<int>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("FoodName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Code");

                    b.ToTable("Food");
                });

            modelBuilder.Entity("EmployeeMG.Data.Entities.TFoodOffered", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("Day")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Food")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Meal")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("OfferedDate")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("FoodOffered");
                });

            modelBuilder.Entity("EmployeeMG.Data.Entities.TGoods", b =>
                {
                    b.Property<int>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Code");

                    b.ToTable("Goods");
                });

            modelBuilder.Entity("EmployeeMG.Data.Entities.TIo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("DateOfDay")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("EntranceTime")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ExitTime")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("PersonnelCode")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TIo");
                });

            modelBuilder.Entity("EmployeeMG.Data.Entities.TLendingGoods", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("GoodsCode")
                        .HasColumnType("int");

                    b.Property<int>("PersonnelCode")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("LendingGoods");
                });

            modelBuilder.Entity("EmployeeMG.Data.Entities.TNUTritionCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<int>("Balance")
                        .HasColumnType("int");

                    b.Property<int>("PersonnelCode")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonnelCode")
                        .IsUnique();

                    b.ToTable("NUTritionCard");
                });

            modelBuilder.Entity("EmployeeMG.Data.Entities.TReservationFood", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<int>("OfferedCode")
                        .HasColumnType("int");

                    b.Property<int>("PersonnelCode")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ReservationFood");
                });

            modelBuilder.Entity("EmployeeMG.Data.Entities.TSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<int>("VacationDay")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Setting");
                });

            modelBuilder.Entity("EmployeeMG.Data.Entities.TShift", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<int>("PersonnelCode")
                        .HasColumnType("int");

                    b.Property<string>("ShiftDate")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ShiftDay")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ShiftTime")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Shift");
                });

            modelBuilder.Entity("EmployeeMG.Data.Entities.TUnit", b =>
                {
                    b.Property<int>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Code");

                    b.ToTable("Unit");
                });

            modelBuilder.Entity("EmployeeMG.Data.Entities.TVacation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<int>("CountDate")
                        .HasColumnType("int");

                    b.Property<string>("FromDate")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("PersonnelCode")
                        .HasColumnType("int");

                    b.Property<int>("PersonnelCodeSuccessor")
                        .HasColumnType("int");

                    b.Property<string>("inmonth")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Vacation");
                });

            modelBuilder.Entity("EmployeeMG.Data.Entities.TAmountOfSalary", b =>
                {
                    b.HasOne("EmployeeMG.Data.Entities.TUnit", "TUnit")
                        .WithOne("TAmountOfSalary")
                        .HasForeignKey("EmployeeMG.Data.Entities.TAmountOfSalary", "UnitId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("TUnit");
                });

            modelBuilder.Entity("EmployeeMG.Data.Entities.TBankAccount", b =>
                {
                    b.HasOne("EmployeeMG.Data.Entities.TEmployee", "Employee")
                        .WithOne("BankAccount")
                        .HasForeignKey("EmployeeMG.Data.Entities.TBankAccount", "PersonnelCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("EmployeeMG.Data.Entities.TEmployee", b =>
                {
                    b.HasOne("EmployeeMG.Data.Entities.TUnit", "TUnit")
                        .WithMany("TEmployees")
                        .HasForeignKey("Unitid")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("TUnit");
                });

            modelBuilder.Entity("EmployeeMG.Data.Entities.TNUTritionCard", b =>
                {
                    b.HasOne("EmployeeMG.Data.Entities.TEmployee", "TEmployee")
                        .WithOne("TritionCard")
                        .HasForeignKey("EmployeeMG.Data.Entities.TNUTritionCard", "PersonnelCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TEmployee");
                });

            modelBuilder.Entity("EmployeeMG.Data.Entities.TEmployee", b =>
                {
                    b.Navigation("BankAccount");

                    b.Navigation("TritionCard");
                });

            modelBuilder.Entity("EmployeeMG.Data.Entities.TUnit", b =>
                {
                    b.Navigation("TAmountOfSalary");

                    b.Navigation("TEmployees");
                });
#pragma warning restore 612, 618
        }
    }
}
