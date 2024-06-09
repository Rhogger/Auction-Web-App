using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Auction.Core.Models;

namespace Auction.Core.Requests.Items;

public class UpdateItemRequest : Request
{
  [JsonIgnore]
  public long Id { get; set; }

  [Required(ErrorMessage = "Nome é obrigatório")]
  [MaxLength(60, ErrorMessage = "O nome do item deve conter pelo menos 60 caracteres")]
  public string Name { get; set; } = string.Empty;
  public string? Description { get; set; }

  [Required(ErrorMessage = "Informe um lance inicial")]
  public decimal InicialBidValue { get; set; } = 0;

  [Required(ErrorMessage = "Data final do leilão deste item é obrigatório")]
  public DateTime TimeEndAuction { get; set; }
}