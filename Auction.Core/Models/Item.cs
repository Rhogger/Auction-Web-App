namespace Auction.Core.Models;

public class Item
{
  public long Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public string? Description { get; set; }
  public decimal InicialBidValue { get; set; } = 0;
  public DateTime CreatedAt { get; set; } = DateTime.Now;
  public DateTime TimeEndAuction { get; set; }
  public List<Bid>? Bids { get; set; } = null;
  public Bid? HighestBid { get; set; } = null;
}
