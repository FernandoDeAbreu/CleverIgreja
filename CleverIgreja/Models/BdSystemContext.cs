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

        public DbSet<CleverIgreja.Models.Igreja> Igreja { get; set; } = default!;

        public DbSet<CleverIgreja.Models.Financeiro> Financeiro { get; set; } = default!;

        public DbSet<CleverIgreja.Models.Moeda> Moeda { get; set; } = default!;

        public DbSet<CleverIgreja.Models.Categoria> Categoria { get; set; } = default!;
    }
}
