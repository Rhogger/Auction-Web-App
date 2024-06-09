using System.Text.Json.Serialization;

namespace Auction.Core.Models;

public class Bid
{
  public long Id { get; set; }
  public long BidderId { get; set; }
  public Bidder Bidder { get; set; } = null!;
  public long ItemFK { get; set; }

  [JsonIgnore]
  public Item Item { get; set; } = null!;
  public decimal BidValue { get; set; } = 0;
}
