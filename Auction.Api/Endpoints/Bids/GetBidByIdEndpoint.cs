using Auction.Api.Common.Api;
using Auction.Core.Handlers;
using Auction.Core.Models;
using Auction.Core.Requests;
using Auction.Core.Responses;

namespace Auction.Api.Endpoints.Bids;

public class GetBidByIdEndpoint : IEndpoint
{
  public static void Map(IEndpointRouteBuilder app)
  => app.MapGet("/{id}", HandleAsync)
        .WithName("Bids: Get by id")
        .WithSummary("Get bid by id")
        .WithDescription("Get bid by id")
        .WithOrder(5)
        .Produces<Response<Bid?>>();

  public static async Task<IResult> HandleAsync(IBidHandler handler, long id)
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