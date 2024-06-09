using Auction.Api.Common.Api;
using Auction.Core.Handlers;
using Auction.Core.Models;
using Auction.Core.Requests.Bidders;
using Auction.Core.Responses;

namespace Auction.Api.Endpoints.Bidders;

public class UpdateBidderEndpoint : IEndpoint
{
  public static void Map(IEndpointRouteBuilder app)
  => app.MapPut("/{id}", HandleAsync)
        .WithName("Bidders: Update")
        .WithSummary("Update a bidder")
        .WithDescription("Update a bidder")
        .WithOrder(2)
        .Produces<Response<Bidder?>>();

  private static async Task<IResult> HandleAsync(IBidderHandler handler,
                                                  UpdateBidderRequest request,
                                                  long id)
  {
    request.Id = id;

    var response = await handler.UpdateAsync(request);

    return response.IsSuccess
      ? TypedResults.Ok(response)
      : TypedResults.BadRequest(response);
  }
}