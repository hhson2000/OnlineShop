using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Model.EF
{
    public partial class BaloShopDbContext : DbContext
    {
        public BaloShopDbContext()
            : base("name=BaloShopDbContext")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Account_Detail> Account_Detail { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Order_Detail> Order_Detail { get; set; }
        public virtual DbSet<Other_Address> Other_Address { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Status_Account> Status_Account { get; set; }
        public virtual DbSet<Status_Order> Status_Order { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Status_Product> Status_Product { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(e => e.Total_Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Order_Detail>()
                .Property(e => e.Product_Price)
                .HasPrecision(18, 0);
        }

        public System.Data.Entity.DbSet<Model.ViewModel.ProductViewModel> ProductViewModels { get; set; }
    }
}
