
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

        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=cuentasporcobrar;Integrated Security=true; MultipleActiveResultsets=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Transaction>()
            .HasOne(c => c.Customer)
            .WithMany(t => t.Transactions)
            .HasForeignKey(c=>c.CustomerId);

        modelBuilder.Entity<Transaction>()
            .HasOne(d => d.Document)
            .WithMany(t => t.Transactions);

       
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
