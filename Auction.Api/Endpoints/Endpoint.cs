using Auction.Api.Common.Api;
using Auction.Api.Endpoints.Bidders;
using Auction.Api.Endpoints.Bids;
using Auction.Api.Endpoints.Items;

namespace Auction.Api.Endpoints;

public static class Endpoint
{
  public static void MapEndpoints(this WebApplication app)
  {
    var endpoints = app.MapGroup("");

    endpoints.MapGroup("v1/bidders")
              .WithTags("Bidders")
              .MapEndpoint<CreateBidderEndpoint>()
              .MapEndpoint<UpdateBidderEndpoint>()
              .MapEndpoint<DeleteBidderEndpoint>()
              .MapEndpoint<GetAllBiddersEndpoint>()
              .MapEndpoint<GetAllPagedBiddersEndpoint>()
              .MapEndpoint<GetBidderByIdEndpoint>();

    endpoints.MapGroup("v1/items")
              .WithTags("Items")
              .MapEndpoint<CreateItemEndpoint>()
              .MapEndpoint<UpdateItemEndpoint>()
              .MapEndpoint<DeleteItemEndpoint>()
              .MapEndpoint<GetAllPagedItemsEndpoint>()
              .MapEndpoint<GetItemByIdEndpoint>();

    endpoints.MapGroup("v1/bids")
              .WithTags("Bids")
              .MapEndpoint<CreateBidEndpoint>()
              .MapEndpoint<DeleteBidEndpoint>()
              .MapEndpoint<GetAllBidsEndpoint>()
              .MapEndpoint<GetBidByIdEndpoint>();
  }

  private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
    where TEndpoint : IEndpoint
  {
    TEndpoint.Map(app);
    return app;
  }
}