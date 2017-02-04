namespace DesignPatterns.Data
{
    using System.Data.Entity;

    public class DesignPatternsDbContext : DbContext
    {
        public DesignPatternsDbContext()
            : base("DefaultConnection")
        {
        }

        public virtual IDbSet<Facture> Factures { get; set; }

        public static DesignPatternsDbContext Create()
        {
            return new DesignPatternsDbContext();
        }
    }
}