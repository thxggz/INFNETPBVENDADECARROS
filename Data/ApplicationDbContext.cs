using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using INFNETPBVENDADECARROS.Models;


namespace INFNETPBVENDADECARROS.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Veiculo> Veiculo { get; set; }
    protected override void OnConfiguring
(
    DbContextOptionsBuilder optionsBuilder
)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

          string conn = config.GetConnectionString("MyConn");
        optionsBuilder.UseSqlServer(conn);
    }
}
