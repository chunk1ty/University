using DesignPatterns.Data;

namespace DesignPatterns.Patterns.Singlethon
{
    public class DdAcessSinglethon
    {
        private static DdAcessSinglethon instance;

        private DesignPatternsDbContext dbContext = new DesignPatternsDbContext();

        private DdAcessSinglethon()
        {
        }

        public static DdAcessSinglethon Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DdAcessSinglethon();
                }

                return instance;
            }
        }

        public DesignPatternsDbContext GetContext()
        {
            return dbContext;
        }

        public void AddFacture(Facture facture)
        {
            this.dbContext.Factures.Add(facture);
            this.dbContext.SaveChanges();
        }

        public void Save()
        {
            this.dbContext.SaveChanges();
        }
    }
}