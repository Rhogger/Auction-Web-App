using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Auction.Core.Requests.Bidders;

public class UpdateBidderRequest : Request
{
  [JsonIgnore]
  public long Id { get; set; }

  [Required(ErrorMessage = "Nome é obrigatório")]
  [MaxLength(30, ErrorMessage = "O nome do comprador deve conter pelo menos 30 caracteres")]
  public string Name { get; set; } = string.Empty;
}