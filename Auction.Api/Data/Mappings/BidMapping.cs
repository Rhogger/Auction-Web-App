using Auction.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Auction.Api.Data.Mappings;

public class BidMapping : IEntityTypeConfiguration<Bid>
{
  public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Bid> builder)
  {
    builder.ToTable("Bids");
    builder.HasKey(x => x.Id);

    builder.Property(x => x.BidValue)
      .IsRequired()
      .HasColumnType("MONEY");

    builder.HasOne(x => x.Item)
      .WithMany(x => x.Bids)
      .HasForeignKey(x => x.ItemFK)
      .IsRequired()
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(x => x.Bidder)
      .WithMany()
      .HasForeignKey(x => x.BidderId)
      .IsRequired()
      .OnDelete(DeleteBehavior.Cascade);
  }
}