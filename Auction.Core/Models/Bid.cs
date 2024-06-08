namespace Auction.Core.Models;

public class Bid
{
  public int Id { get; set; }
  public Bidder Bidder { get; set; } = null!;
  public string ItemId { get; set; } = string.Empty;
  public decimal BidValue { get; set; } = 0;
}
