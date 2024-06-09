using Auction.Api.Common.Api;
using Auction.Core.Handlers;
using Auction.Core.Models;
using Auction.Core.Requests;
using Auction.Core.Responses;

namespace Auction.Api.Endpoints.Items;

public class DeleteItemEndpoint : IEndpoint
{
  public static void Map(IEndpointRouteBuilder app)
  => app.MapDelete("/{id}", HandleAsync)
        .WithName("Items: Delete")
        .WithSummary("Delete a item")
        .WithDescription("Delete a item")
        .WithOrder(3)
        .Produces<Response<Item?>>();

  private static async Task<IResult> HandleAsync(IItemHandler handler, long id)
  {
    var request = new DeleteRequest { Id = id };

    var response = await handler.DeleteAsync(request);

    return response.IsSuccess
      ? TypedResults.Ok(response)
      : TypedResults.BadRequest(response);
  }
}