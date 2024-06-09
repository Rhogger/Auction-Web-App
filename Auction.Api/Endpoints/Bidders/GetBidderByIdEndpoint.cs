using Auction.Api.Common.Api;
using Auction.Core.Handlers;
using Auction.Core.Models;
using Auction.Core.Requests;
using Auction.Core.Responses;

namespace Auction.Api.Endpoints.Bidders;

public class GetBidderByIdEndpoint : IEndpoint
{
  public static void Map(IEndpointRouteBuilder app)
  => app.MapGet("/{id}", HandleAsync)
        .WithName("Bidders: Get by id")
        .WithSummary("Get bidder by id")
        .WithDescription("Get bidder by id")
        .WithOrder(6)
        .Produces<Response<Bidder?>>();

  public static async Task<IResult> HandleAsync(IBidderHandler handler, long id)
  {
    var request = new GetByIdRequest
    {
      Id = id
    };

    var response = await handler.GetByIdAsync(request);

    return response.IsSuccess
      ? TypedResults.Ok(response)
      : TypedResults.BadRequest(response);
  }
}