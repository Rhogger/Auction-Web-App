using System.ComponentModel.DataAnnotations;
using Auction.Core.Models;

namespace Auction.Core.Requests.Bids;

public class CreateBidRequest : Request
{
  [Required(ErrorMessage = "Lance nao vinculado a um comprador")]
  public Bidder BidderId { get; set; } = null!;

  [Required(ErrorMessage = "Lance nao vinculado a um item")]
  public string ItemId { get; set; } = string.Empty;

  [Required(ErrorMessage = "Valor inválido para um lance")]
  public decimal BidValue { get; set; } = 0;
}