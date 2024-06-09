using Auction.Api.Common.Api;
using Auction.Core;
using Auction.Core.Handlers;
using Auction.Core.Models;
using Auction.Core.Requests;
using Auction.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Auction.Api.Endpoints.Bidders;

public class GetAllPagedBiddersEndpoint : IEndpoint
{
  public static void Map(IEndpointRouteBuilder app)
    => app.MapGet("/list", HandleAsync)
          .WithName("Bidders: Get All Paged")
          .WithSummary("Get all paged bidders")
          .WithDescription("Get all paged bidders")
          .WithOrder(5)
          .Produces<PagedResponse<List<Bidder>?>>();

  public static async Task<IResult> HandleAsync(IBidderHandler handler,
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