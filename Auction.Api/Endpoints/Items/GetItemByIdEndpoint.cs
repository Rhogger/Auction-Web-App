using Auction.Api.Common.Api;
using Auction.Core.Handlers;
using Auction.Core.Models;
using Auction.Core.Requests;
using Auction.Core.Responses;

namespace Auction.Api.Endpoints.Items;

public class GetItemByIdEndpoint : IEndpoint
{
  public static void Map(IEndpointRouteBuilder app)
  => app.MapGet("/{id}", HandleAsync)
        .WithName("Items: Get by id")
        .WithSummary("Get item by id")
        .WithDescription("Get item by id")
        .WithOrder(5)
        .Produces<Response<Item?>>();

  public static async Task<IResult> HandleAsync(IItemHandler handler, long id)
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