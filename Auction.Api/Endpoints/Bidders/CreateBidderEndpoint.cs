using Auction.Api.Common.Api;
using Auction.Core.Handlers;
using Auction.Core.Models;
using Auction.Core.Requests.Bidders;
using Auction.Core.Responses;

namespace Auction.Api.Endpoints.Bidders;

public class CreateBidderEndpoint : IEndpoint
{
  public static void Map(IEndpointRouteBuilder app)
  => app.MapPost("/", HandleAsync)
        .WithName("Bidders: Create")
        .WithSummary("Create a new bidder")
        .WithDescription("Create a new bidder")
        .WithOrder(1)
        .Produces<Response<Bidder?>>();

  private static async Task<IResult> HandleAsync(IBidderHandler handler, CreateBidderRequest request)
  {
    var response = await handler.CreateAsync(request);

    return response.IsSuccess
      ? TypedResults.Created($"v1/bidders/{response.Data?.Id}", response)
      : TypedResults.BadRequest(response);
  }
}