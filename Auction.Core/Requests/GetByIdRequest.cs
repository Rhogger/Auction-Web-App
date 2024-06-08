namespace Auction.Core.Requests.Items;

public class GetByIdRequest : Request
{
  public long Id { get; set; }
}