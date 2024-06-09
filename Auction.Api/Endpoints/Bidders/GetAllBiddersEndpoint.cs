using Auction.Api.Common.Api;
using Auction.Core.Handlers;
using Auction.Core.Models;
using Auction.Core.Requests;
using Auction.Core.Responses;

namespace Auction.Api.Endpoints.Bidders;

public class GetAllBiddersEndpoint : IEndpoint
{
  public static void Map(IEndpointRouteBuilder app)
  => app.MapGet("/", HandleAsync)
        .WithName("Bidders: Get All")
        .WithSummary("Get all bidders")
        .WithDescription("Get all bidders")
        .WithOrder(4)
        .Produces<Response<List<Bidder>?>>();

  public static async Task<IResult> HandleAsync(IBidderHandler handler)
  {
    var request = new GetAllRequest();

    var response = await handler.GetAllAsync(request);

    return response.IsSuccess
      ? TypedResults.Ok(response)
      : TypedResults.BadRequest(response);
  }
}