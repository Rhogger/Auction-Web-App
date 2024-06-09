using Auction.Api.Common.Api;
using Auction.Core.Handlers;
using Auction.Core.Models;
using Auction.Core.Requests.Bids;
using Auction.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Auction.Api.Endpoints.Bids;

public class GetAllBidsEndpoint : IEndpoint
{
  public static void Map(IEndpointRouteBuilder app)
  => app.MapGet("/", HandleAsync)
        .WithName("Bids: Get All by item")
        .WithSummary("Get all bids by item")
        .WithDescription("Get all bids by item")
        .WithOrder(3)
        .Produces<Response<List<Bid>?>>();

  public static async Task<IResult> HandleAsync(IBidHandler handler, long ItemId)
  {
    var request = new GetAllBidRequest
    {
      ItemId = ItemId
    };

    var response = await handler.GetAllByItemAsync(request);

    return response.IsSuccess
      ? TypedResults.Ok(response)
      : TypedResults.BadRequest(response);
  }
}