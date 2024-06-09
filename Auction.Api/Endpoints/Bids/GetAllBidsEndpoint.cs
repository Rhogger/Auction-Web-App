using Auction.Api.Common.Api;
using Auction.Core.Handlers;
using Auction.Core.Models;
using Auction.Core.Requests.Bids;
using Auction.Core.Responses;

namespace Auction.Api.Endpoints.Bids;

public class GetAllBidsEndpoint : IEndpoint
{
  public static void Map(IEndpointRouteBuilder app)
  => app.MapGet("/", HandleAsync)
        .WithName("Bids: Get All")
        .WithSummary("Get all bid")
        .WithDescription("Get all bid")
        .WithOrder(4)
        .Produces<Response<List<Bid>?>>();

  public static async Task<IResult> HandleAsync(IBidHandler handler)
  {
    var request = new GetAllBidRequest();

    var response = await handler.GetAllAsync(request);

    return response.IsSuccess
      ? TypedResults.Ok(response)
      : TypedResults.BadRequest(response);
  }
}