namespace mvc_proboosting.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BoostTaskModel : DbContext
    {
        public BoostTaskModel()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<Booster> Boosters { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booster>()
                .HasMany(e => e.Customers)
                .WithRequired(e => e.Booster)
                .WillCascadeOnDelete(false);
        }
    }
}
