using DokumentCodeFirst.Entity;
using Microsoft.EntityFrameworkCore;

namespace DokumentCodeFirst.Data
{
    public class SqlContext: DbContext
    {
        public SqlContext(DbContextOptions options): base(options) { }
        public DbSet<Dokument> Dokument { get; set; }
        public DbSet<Klijent> Klijent { get; set; }
        public DbSet<StavkeDokumenta> StavkeDokumenta { get; set; }
        public DbSet<Proizvod> Proizvod { get; set; }
        public DbSet<TipDokumenta> TipDokumenta { get; set; }
    }
}
