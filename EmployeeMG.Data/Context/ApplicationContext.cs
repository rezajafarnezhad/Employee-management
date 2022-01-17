using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMG.Data.Entities;
using EmployeeMG.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMG.Data.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            
        }

        public ApplicationContext()
        {
            
        }

        public DbSet<TCompany> Company { get; set; }
        public DbSet<TUnit> Unit { get; set; }
        public DbSet<TEmployee> Employee { get; set; }
        public DbSet<TIo> TIo { get; set; }
        public DbSet<TVacation> Vacation { get; set; }
        public DbSet<TShift> Shift  { get; set; }
        public DbSet<TBankAccount> BankAccount { get; set; }
        public DbSet<TDepositSalary> DepositSalary { get; set; }
        public DbSet<TCost> Cost { get; set; }
        public DbSet<TAmountOfSalary> AmountOfSalary { get; set; }
        public DbSet<TFood> Food { get; set; }
        public DbSet<TFoodOffered> FoodOffered { get; set; }
        public DbSet<TReservationFood> ReservationFood { get; set; }
        public DbSet<TNUTritionCard> NUTritionCard { get; set; }
        public DbSet<TGoods> Goods { get; set; }
        public DbSet<TLendingGoods> LendingGoods { get; set; }
        public DbSet<TSetting> Setting { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var asb = typeof(TCompanyMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(asb);

            base.OnModelCreating(modelBuilder);
        }
    }
}
