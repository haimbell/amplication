using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Service_1.Infrastructure.Models;

namespace Service_1.Infrastructure;

public class Service_1DbContext : IdentityDbContext<IdentityUser>
{
    public Service_1DbContext(DbContextOptions<Service_1DbContext> options)
        : base(options) { }

    public DbSet<Click> Clicks { get; set; }

    public DbSet<Ad> Ads { get; set; }

    public DbSet<Advertiser> Advertisers { get; set; }

    public DbSet<Campaign> Campaigns { get; set; }

    public DbSet<Impression> Impressions { get; set; }

    public DbSet<Conversion> Conversions { get; set; }

    public DbSet<Publisher> Publishers { get; set; }

    public DbSet<User> Users { get; set; }
}
