using System.ComponentModel.DataAnnotations;
using Auction.Core.Models;

namespace Auction.Core.Requests.Bids;

public class CreateBidRequest : Request
{
  [Required(ErrorMessage = "Lance nao vinculado a um comprador")]
  public long BidderId { get; set; }

  [Required(ErrorMessage = "Lance nao vinculado a um item")]
  public long ItemId { get; set; }

  [Required(ErrorMessage = "Valor inválido para um lance")]
  public decimal BidValue { get; set; } = 0;
}