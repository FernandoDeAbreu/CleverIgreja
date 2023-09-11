using Microsoft.EntityFrameworkCore;
using CleverIgreja.Models;

namespace CleverIgreja.Models
{
    public partial class BdSystemContext : DbContext
    {
        public BdSystemContext()
        {
        }

        public BdSystemContext(DbContextOptions<BdSystemContext> options)
            : base(options)
        {
        }

        public DbSet<CleverIgreja.Models.Membro> Membro { get; set; }
    }
}
