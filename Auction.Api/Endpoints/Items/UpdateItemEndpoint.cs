using Auction.Api.Common.Api;
using Auction.Core.Handlers;
using Auction.Core.Models;
using Auction.Core.Requests.Items;
using Auction.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Auction.Api.Endpoints.Items;

public class UpdateItemEndpoint : IEndpoint
{
  public static void Map(IEndpointRouteBuilder app)
  => app.MapPut("/{id}", HandleAsync)
        .WithName("Items: Update")
        .WithSummary("Update a item")
        .WithDescription("Update a item")
        .WithOrder(2)
        .Produces<Response<Item?>>();

  private static async Task<IResult> HandleAsync(IItemHandler handler,
                                                  UpdateItemRequest request,
                                                  long id)
  {
    request.Id = id;

    var response = await handler.UpdateAsync(request);

    return response.IsSuccess
      ? TypedResults.Ok(response)
      : TypedResults.BadRequest(response);
  }
}