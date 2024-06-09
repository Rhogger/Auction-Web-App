using Auction.Api.Common.Api;
using Auction.Core.Handlers;
using Auction.Core.Models;
using Auction.Core.Requests;
using Auction.Core.Responses;

namespace Auction.Api.Endpoints.Bidders;

public class DeleteBidderEndpoint : IEndpoint
{
  public static void Map(IEndpointRouteBuilder app)
  => app.MapDelete("/{id}", HandleAsync)
        .WithName("Bidders: Delete")
        .WithSummary("Delete a bidder")
        .WithDescription("Delete a bidder")
        .WithOrder(3)
        .Produces<Response<Bidder?>>();

  private static async Task<IResult> HandleAsync(IBidderHandler handler, long id)
  {
    var request = new DeleteRequest { Id = id };

    var response = await handler.DeleteAsync(request);

    return response.IsSuccess
      ? TypedResults.Ok(response)
      : TypedResults.BadRequest(response);
  }
}