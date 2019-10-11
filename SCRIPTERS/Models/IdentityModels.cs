using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SCRIPTERS.Core;
using SCRIPTERS.Core.Models;
using SCRIPTERS.Core.Models.Operation;
using SCRIPTERS.Core.Models.ViewModel;

namespace SCRIPTERS.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> 
    {
        public DbSet<ExpenseItem> ExpenseItems { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<InventoryCategory> InventoryCategories { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Outlet> Outlets { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Sale> Sales { get; set; }
        public DbSet<SalesDetail> SalesDetails { get; set; }
        public DbSet<InventorySale> InventorySales { get; set; }
        public DbSet<InventorySalesDetail> InventorySalesDetails { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseDetail> PurchaseDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseDetail> ExpenseDetails { get; set; }
        public DbSet<RoleType> RoleTypes { get; set; }

      
        public DbSet<IncomeVm> IncomeVms { get; set; }
        public DbSet<InventoryIncomeVm> InventoryIncomeVms { get; set; }

        public DbSet<Audit> Audits { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<SmsModel> SmsModels { get; set; }
        public DbSet<EmailModel> EmailModels { get; set; }
         public DbSet<BusinessRule> BusinessRules { get; set; }

       



        public ApplicationDbContext()
            : base("ShopDbContext", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Purchase>()
                .HasRequired(d => d.Outlet)
                .WithMany(w => w.Purchases)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasRequired(d => d.Outlet)
                .WithMany(w => w.Orders)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sale>()
                .HasRequired(d => d.Outlet)
                .WithMany(w => w.Sales)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<InventorySale>()
                .HasRequired(d => d.Outlet)
                .WithMany(w => w.InventorySale)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Expense>()
                .HasRequired(d => d.Employee)
                .WithMany(w => w.Expenses)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}