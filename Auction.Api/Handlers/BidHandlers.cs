using Auction.Api.Data;
using Auction.Api.Endpoints.Bidders;
using Auction.Core.Handlers;
using Auction.Core.Models;
using Auction.Core.Requests;
using Auction.Core.Requests.Bids;
using Auction.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Auction.Api.Handlers;

public class BidHandler(AppDbContext context) : IBidHandler
{
  public async Task<Response<Bid?>> CreateAsync(CreateBidRequest request)
  {
    try
    {
      var bidderHandler = new BidderHandler(context);

      var getByIdBidderRequest = new GetByIdRequest
      {
        Id = request.BidderId
      };

      var bidderResponse = await bidderHandler.GetByIdAsync(getByIdBidderRequest);

      var bidder = bidderResponse.Data;

      var bid = new Bid
      {
        BidderId = request.BidderId,
        Bidder = bidder,
        ItemFK = request.ItemId,
        BidValue = request.BidValue,
      };

      await context.Bids.AddAsync(bid);
      await context.SaveChangesAsync();
      return new Response<Bid?>(bid, 201, "Bid succesfully created.");
    }
    catch
    {
      return new Response<Bid?>(null, 500, "The Bid could not be created.");
    }
  }

  public async Task<Response<Bid?>> UpdateAsync(UpdateBidRequest request)
  {
    try
    {
      var bid = await context.Bids.FirstOrDefaultAsync(x => x.Id == request.Id);

      if (bid == null)
        return new Response<Bid?>(null, 404, "Bid not found.");

      bid.BidValue = request.BidValue;

      context.Bids.Update(bid);
      await context.SaveChangesAsync();

      return new Response<Bid?>(bid, message: "Bid succesfully updated.");
    }
    catch
    {
      return new Response<Bid?>(null, 500, "The bid could not be updated.");
    }
  }

  public async Task<Response<Bid?>> DeleteAsync(DeleteRequest request)
  {
    try
    {
      var bid = await context.Bids.FirstOrDefaultAsync(x => x.Id == request.Id);

      if (bid == null)
        return new Response<Bid?>(null, 404, "Bid not found.");

      context.Bids.Remove(bid);
      await context.SaveChangesAsync();

      return new Response<Bid?>(bid, message: "Bid succesfully removed.");
    }
    catch
    {
      return new Response<Bid?>(null, 500, "The bid could not be removed.");
    }
  }

  public async Task<Response<List<Bid>?>> GetAllAsync(GetAllBidRequest request)
  {
    try
    {
      var bids = await context
                              .Bids
                              .AsNoTracking()
                              .Where(x => x.BidderId == request.BidderId && x.ItemFK == request.ItemId)
                              .OrderBy(x => x.BidValue)
                              .ToListAsync();

      return bids.Count() == 0
        ? new Response<List<Bid>?>(null, 404, "No one bid not found.")
        : new Response<List<Bid>?>(bids);
    }
    catch
    {
      return new Response<List<Bid>?>(null, 500, "No one bid could not be found.");
    }
  }

  public async Task<Response<Bid?>> GetByIdAsync(GetByIdRequest request)
  {
    try
    {
      var bid = await context.Bids.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id);

      return bid is null
        ? new Response<Bid?>(null, 404, "Bid not found.")
        : new Response<Bid?>(bid);
    }
    catch
    {
      return new Response<Bid?>(null, 500, "The bid could not be found.");
    }
  }
}