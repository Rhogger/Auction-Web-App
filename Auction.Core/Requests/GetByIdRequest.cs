namespace Auction.Core.Requests;

public class GetByIdRequest : Request
{
  public long Id { get; set; }
}