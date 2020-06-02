using System.Data.Entity;

namespace InsuranceComp
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext() : base("DefaultConnection") { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<InsType> InsTypes { get; set; }
        public virtual DbSet<Insurance> Insurances { get; set; }
        public virtual DbSet<Incident> Incidents { get; set; }
        public virtual DbSet<Payout> Payouts { get; set; }
    }
}
