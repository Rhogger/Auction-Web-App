using Auction.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Auction.Api.Data.Mappings;

public class ItemMapping : IEntityTypeConfiguration<Item>
{
  public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Item> builder)
  {
    builder.ToTable("Items");
    builder.HasKey(x => x.Id);

    builder.Property(x => x.Name)
      .IsRequired()
      .HasColumnType("NVARCHAR")
      .HasMaxLength(60);

    builder.Property(x => x.Description)
      .IsRequired(false)
      .HasColumnType("NVARCHAR")
      .HasMaxLength(255);

    builder.Property(x => x.InicialBidValue)
      .IsRequired()
      .HasColumnType("MONEY");

    builder.Property(x => x.CreatedAt)
      .IsRequired();

    builder.Property(x => x.TimeEndAuction)
      .IsRequired();

    builder.HasMany(x => x.Bids)
      .WithOne(x => x.Item)
      .HasForeignKey(x => x.ItemFK)
      .OnDelete(DeleteBehavior.Cascade);
  }
}