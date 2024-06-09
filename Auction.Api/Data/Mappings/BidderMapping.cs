using Auction.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Auction.Api.Data.Mappings;

public class BidderMapping : IEntityTypeConfiguration<Bidder>
{
  public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Bidder> builder)
  {
    builder.ToTable("Bidders");
    builder.HasKey(x => x.Id);

    builder.Property(x => x.Name)
      .IsRequired()
      .HasColumnType("NVARCHAR")
      .HasMaxLength(30);
  }
}