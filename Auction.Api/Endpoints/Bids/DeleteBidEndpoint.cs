using Auction.Api.Common.Api;
using Auction.Core.Handlers;
using Auction.Core.Models;
using Auction.Core.Requests;
using Auction.Core.Responses;

namespace Auction.Api.Endpoints.Bids;

public class DeleteBidEndpoint : IEndpoint
{
  public static void Map(IEndpointRouteBuilder app)
  => app.MapDelete("/{id}", HandleAsync)
        .WithName("Bid: Delete")
        .WithSummary("Delete a bid")
        .WithDescription("Delete a bid")
        .WithOrder(2)
        .Produces<Response<Bid?>>();

  private static async Task<IResult> HandleAsync(IBidHandler handler, long id)
  {
    var request = new DeleteRequest { Id = id };

    var response = await handler.DeleteAsync(request);

    return response.IsSuccess
      ? TypedResults.Ok(response)
      : TypedResults.BadRequest(response);
  }
}