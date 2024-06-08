using System.ComponentModel.DataAnnotations;
using Auction.Core.Models;

namespace Auction.Core.Requests.Items;

public class UpdateItemRequest : Request
{
  public long Id { get; set; }

  [Required(ErrorMessage = "Nome é obrigatório")]
  [MaxLength(60, ErrorMessage = "O nome do item deve conter pelo menos 60 caracteres")]
  public string Name { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;

  [Required(ErrorMessage = "Informe um lance inicial")]
  public decimal InicialBidValue { get; set; } = 0;
  public DateTime CreatedAt { get; set; } = DateTime.Now;
  public DateTime TimeEndAuction { get; set; } = DateTime.Now.AddDays(3);
  public List<Bid>? Bids { get; set; } = null;
  public Bid? HighestBid { get; set; } = null;
}