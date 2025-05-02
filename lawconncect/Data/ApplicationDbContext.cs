using lawconncect.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace lawconncect.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Adalot> adalots { get; set; }
    public DbSet<Case> Cases { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<Lawyer> Lawyers { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Master> Masters { get; set; }
    public DbSet<Details> details { get; set; }
    public DbSet<Documents> documents { get; set; }

    public DbSet<LawyerWithCase> LawyerWithCases { get; set; }  

}
