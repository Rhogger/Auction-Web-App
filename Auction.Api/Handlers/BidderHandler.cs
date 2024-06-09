using Auction.Api.Data;
using Auction.Core.Handlers;
using Auction.Core.Models;
using Auction.Core.Requests;
using Auction.Core.Requests.Bidders;
using Auction.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Auction.Api.Handlers;

public class BidderHandler(AppDbContext context) : IBidderHandler
{
  public async Task<Response<Bidder?>> CreateAsync(CreateBidderRequest request)
  {
    var bidder = new Bidder
    {
      Name = request.Name
    };

    try
    {
      await context.Bidders.AddAsync(bidder);
      await context.SaveChangesAsync();
      return new Response<Bidder?>(bidder, 201, "Bidder succesfully created.");
    }
    catch
    {
      return new Response<Bidder?>(null, 500, "The bidder could not be created.");
    }
  }

  public async Task<Response<Bidder?>> UpdateAsync(UpdateBidderRequest request)
  {
    try
    {
      var bidder = await context.Bidders.FirstOrDefaultAsync(x => x.Id == request.Id);

      if (bidder == null)
        return new Response<Bidder?>(null, 404, "Bidder not found.");

      bidder.Name = request.Name;

      context.Bidders.Update(bidder);
      await context.SaveChangesAsync();

      return new Response<Bidder?>(bidder, message: "Bidder succesfully updated.");
    }
    catch
    {
      return new Response<Bidder?>(null, 500, "The bidder could not be updated.");
    }
  }

  public async Task<Response<Bidder?>> DeleteAsync(DeleteRequest request)
  {
    try
    {
      var bidder = await context.Bidders.FirstOrDefaultAsync(x => x.Id == request.Id);

      if (bidder == null)
        return new Response<Bidder?>(null, 404, "Bidder not found.");

      context.Bidders.Remove(bidder);
      await context.SaveChangesAsync();

      return new Response<Bidder?>(bidder, message: "Bidder succesfully removed.");
    }
    catch
    {
      return new Response<Bidder?>(null, 500, "The bidder could not be removed.");
    }
  }

  public async Task<Response<Bidder?>> GetByIdAsync(GetByIdRequest request)
  {
    try
    {
      var bidder = await context.Bidders.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id);

      return bidder is null
        ? new Response<Bidder?>(null, 404, "Bidder not found.")
        : new Response<Bidder?>(bidder);
    }
    catch
    {
      return new Response<Bidder?>(null, 500, "The bidder could not be found.");
    }
  }

  public async Task<Response<List<Bidder>?>> GetAllAsync(GetAllRequest request)
  {
    try
    {
      var bidders = await context.Bidders.AsNoTracking().OrderBy(x => x.Name).ToListAsync();

      return bidders.Count() == 0
        ? new Response<List<Bidder>?>(null, 404, "No one bidder not found.")
        : new Response<List<Bidder>?>(bidders);
    }
    catch
    {
      return new Response<List<Bidder>?>(null, 500, "No one bidder could not be found.");
    }
  }

  public async Task<PagedResponse<List<Bidder>?>> GetAllPagedAsync(GetAllPagedRequest request)
  {
    try
    {
      var query = context.Bidders.AsNoTracking().OrderBy(x => x.Name);

      var bidders = await query
                              .Skip((request.PageNumber - 1) * request.PageSize)
                              .Take(request.PageSize)
                              .ToListAsync();

      var count = await query.CountAsync();

      return new PagedResponse<List<Bidder>?>(bidders, count, request.PageNumber, request.PageSize);
    }
    catch
    {
      return new PagedResponse<List<Bidder>?>(null, 500, "The bidder could not be found.");
    }
  }
}