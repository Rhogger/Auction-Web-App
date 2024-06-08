namespace Auction.Core.Requests.Items;

public class GetItemByIdRequest : Request
{
  public long Id { get; set; }
}