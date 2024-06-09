using System.Reflection;
using Auction.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Auction.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
  public DbSet<Item> Items { get; set; } = null!;
  public DbSet<Bidder> Bidders { get; set; } = null!;
  public DbSet<Bid> Bids { get; set; } = null!;

  protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}