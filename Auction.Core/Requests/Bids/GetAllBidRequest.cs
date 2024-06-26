using System.ComponentModel.DataAnnotations;

namespace Auction.Core.Requests.Bids;

public class GetAllBidRequest : Request
{
  [Required(ErrorMessage = "Lance nao vinculado a um item")]
  public long ItemId { get; set; }
}