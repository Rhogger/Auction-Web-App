using Auction.Api.Common.Api;
using Auction.Core.Handlers;
using Auction.Core.Models;
using Auction.Core.Requests.Items;
using Auction.Core.Responses;

namespace Auction.Api.Endpoints.Items;

public class CreateItemEndpoint : IEndpoint
{
  public static void Map(IEndpointRouteBuilder app)
  => app.MapPost("/", HandleAsync)
        .WithName("Items: Create")
        .WithSummary("Create a new item")
        .WithDescription("Create a new item")
        .WithOrder(1)
        .Produces<Response<Item?>>();

  private static async Task<IResult> HandleAsync(IItemHandler handler, CreateItemRequest request)
  {
    var response = await handler.CreateAsync(request);

    return response.IsSuccess
      ? TypedResults.Created($"v1/items/{response.Data?.Id}", response)
      : TypedResults.BadRequest(response);
  }
}