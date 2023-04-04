
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace CuentasPorCobrar.Shared;

public partial class CuentasporcobrardbContext : DbContext
{
    public virtual DbSet<Document> Documents { get; set; } = null!; 
    public virtual DbSet<Customer> Customers { get; set; } = null!; 
    public virtual DbSet<Transaction> Transactions { get; set; } = null!;  
    public virtual DbSet<AccountingEntry> AccountingEntries { get; set; } = null!;  
    public CuentasporcobrardbContext()
    {
    }

    public CuentasporcobrardbContext(DbContextOptions<CuentasporcobrardbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Server=tcp:cuentaporcobrardbserver.database.windows.net,1433;Initial Catalog=cuentasporcobrar;Persist Security Info=False;User ID=moscat;Password=Cuevas00#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Transaction>()
            .HasOne(c => c.Customer)
            .WithMany(t => t.Transactions)
            .HasForeignKey(c => c.CustomerId).OnDelete(DeleteBehavior.Cascade); ;

        modelBuilder.Entity<Transaction>()
            .HasOne(d => d.Document)
            .WithMany(t => t.Transactions).OnDelete(DeleteBehavior.Cascade); ;


        modelBuilder.Entity<AccountingEntry>()
          .HasOne(c => c.Customer)
          .WithMany(t => t.AccountingEntries)
          .HasForeignKey(c => c.CustomerId).OnDelete(DeleteBehavior.Cascade); ;

        //modelBuilder.Entity<Customer>()
        //    .HasMany(c => c.AccountingEntries)
        //    .WithOne(c => c.Customer)
        //    .OnDelete(DeleteBehavior.ClientCascade);

        //modelBuilder.Entity<Customer>()
        //    .HasMany(t => t.Transactions)
        //    .WithOne(c => c.Customer)
        //    .OnDelete(DeleteBehavior.ClientCascade);

        //modelBuilder.Entity<Document>()
        //    .HasMany(c => c.Transactions)
        //    .WithOne(c => c.Document)
        //    .OnDelete(DeleteBehavior.ClientCascade);








        // modelBuilder.Entity<AccountingEntry>()
        //     .Property(x=>x.Customer)
        //     .HasConversion(
        //    x => JsonSerializer.Serialize(x,(JsonSerializerOptions)null),
        //    x => JsonSerializer.Deserialize<Customer>(x,(JsonSerializerOptions)null)
        //);



        //modelBuilder.Entity<AccountingEntry>()
        //    .HasOne(c => c.Customer)
        //    .WithMany(a => a.AccountingEntries)
        //    .HasForeignKey(c=>c.CustomerId);






        OnModelCreatingPartial(modelBuilder);

    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
