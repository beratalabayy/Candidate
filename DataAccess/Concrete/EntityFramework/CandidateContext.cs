
using Entity.Customers;
using Entity.Transactions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class CandidateContext : DbContext
    {
        public CandidateContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=BERATALABEY;Database=Candidate;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

    }
}
