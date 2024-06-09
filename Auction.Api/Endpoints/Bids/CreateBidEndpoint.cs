using Auction.Api.Common.Api;
using Auction.Core.Handlers;
using Auction.Core.Models;
using Auction.Core.Requests.Bids;
using Auction.Core.Responses;

namespace Auction.Api.Endpoints.Bids;

public class CreateBidEndpoint : IEndpoint
{
  public static void Map(IEndpointRouteBuilder app)
  => app.MapPost("/", HandleAsync)
        .WithName("Bids: Create")
        .WithSummary("Create a new bid")
        .WithDescription("Create a new bid")
        .WithOrder(1)
        .Produces<Response<Bid?>>();

  private static async Task<IResult> HandleAsync(IBidHandler handler, CreateBidRequest request)
  {
    var response = await handler.CreateAsync(request);

    return response.IsSuccess
      ? TypedResults.Created($"v1/bids/{response.Data?.Id}", response)
      : TypedResults.BadRequest(response);
  }
}