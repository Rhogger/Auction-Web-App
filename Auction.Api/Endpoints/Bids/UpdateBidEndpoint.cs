using Auction.Api.Common.Api;
using Auction.Core.Handlers;
using Auction.Core.Models;
using Auction.Core.Requests.Bids;
using Auction.Core.Responses;

namespace Auction.Api.Endpoints.Bids;

public class UpdateBidEndpoint : IEndpoint
{
  public static void Map(IEndpointRouteBuilder app)
  => app.MapPut("/{id}", HandleAsync)
        .WithName("Bids: Update")
        .WithSummary("Update a bid")
        .WithDescription("Update a bid")
        .WithOrder(2)
        .Produces<Response<Bid?>>();

  private static async Task<IResult> HandleAsync(IBidHandler handler,
                                                  UpdateBidRequest request,
                                                  long id)
  {
    request.Id = id;

    var response = await handler.UpdateAsync(request);

    return response.IsSuccess
      ? TypedResults.Ok(response)
      : TypedResults.BadRequest(response);
  }
}