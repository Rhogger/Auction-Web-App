using Auction.Api.Common.Api;
using Auction.Core;
using Auction.Core.Handlers;
using Auction.Core.Models;
using Auction.Core.Requests;
using Auction.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Auction.Api.Endpoints.Items;

public class GetAllPagedItemsEndpoint : IEndpoint
{
  public static void Map(IEndpointRouteBuilder app)
    => app.MapGet("/", HandleAsync)
          .WithName("Items: Get All Paged")
          .WithSummary("Get all paged items")
          .WithDescription("Get all paged items")
          .WithOrder(4)
          .Produces<PagedResponse<List<Item>?>>();

  public static async Task<IResult> HandleAsync(IItemHandler handler,
                                                  [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
                                                  [FromQuery] int pageSize = Configuration.DefaultPageSize)
  {
    var request = new GetAllPagedRequest
    {
      PageNumber = pageNumber,
      PageSize = pageSize
    };

    var response = await handler.GetAllPagedAsync(request);

    return response.IsSuccess
      ? TypedResults.Ok(response)
      : TypedResults.BadRequest(response);
  }
}