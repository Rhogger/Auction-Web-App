using System.ComponentModel.DataAnnotations;
using Auction.Core.Models;

namespace Auction.Core.Requests.Bids;

public class UpdateBidRequest : Request
{
  public long Id { get; set; }

  [Required(ErrorMessage = "Valor inválido para um lance")]
  public decimal BidValue { get; set; } = 0;
}