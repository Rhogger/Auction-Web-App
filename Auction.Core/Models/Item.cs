namespace Auction.Core.Models;

public class Item
{
  public long Id { get; set; }
  public string UserId { get; set; } = string.Empty;
  public string Name { get; set; } = string.Empty;
  public string? Description { get; set; }
  public decimal InicialBid { get; set; } = 0;
  public DateTime CreatedAt { get; set; } = DateTime.Now;
  public DateTime TimeEndAuction { get; set; }
  public decimal? HighestBid { get; set; } = 0;
  public string? BidderId { get; set; }
  public string? BidderName { get; set; }
}
