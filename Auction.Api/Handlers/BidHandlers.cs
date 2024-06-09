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
      var item = await context.Items.FindAsync(request.ItemId);

      if (item == null)
        return new Response<Bid?>(null, 404, "Item not found.");

      item = await context
                          .Items
                          .Where(i => i.Id == request.ItemId)
                          .Select(i => new Item
                          {
                            Id = i.Id,
                            TimeEndAuction = i.TimeEndAuction,
                            Bids = i.Bids
                                    .Select(b => new Bid
                                    {
                                      Id = b.Id,
                                      BidValue = b.BidValue
                                    })
                                    .ToList()
                          })
                          .FirstOrDefaultAsync();

      if (item == null)
        return new Response<Bid?>(null, 404, "Item not found.");

      if (item.TimeEndAuction <= DateTime.Now)
        return new Response<Bid?>(null, 404, "Bids cannot be placed on items that have already been auctioned off.");

      var highestBid = item
                          .Bids
                          .Any()
                        ? item
                              .Bids
                              .Max(b => b.BidValue)
                        : (decimal?)null;

      if (highestBid == null || request.BidValue > highestBid)
      {
        var bid = new Bid
        {
          BidderId = request.BidderId,
          ItemFK = request.ItemId,
          BidValue = request.BidValue,
        };

        await context.Bids.AddAsync(bid);
        await context.SaveChangesAsync();
        return new Response<Bid?>(bid, 201, "Bid succesfully created.");
      }
      else
      {
        return new Response<Bid?>(null, 400, "Bid value must be higher than the existing highest bid.");
      }
    }
    catch
    {
      return new Response<Bid?>(null, 500, "The Bid could not be created.");
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

  public async Task<Response<List<Bid>?>> GetAllByItemAsync(GetAllBidRequest request)
  {
    var id = request.ItemId;

    try
    {
      var bids = await context
                              .Bids
                              .AsNoTracking()
                              .Where(x => x.ItemFK == request.ItemId)
                              .OrderBy(x => x.BidValue)
                              .Select(b => new Bid
                              {
                                Id = b.Id,
                                BidderId = b.BidderId,
                                ItemFK = b.ItemFK,
                                BidValue = b.BidValue,
                                Bidder = b.Bidder
                              })
                              .ToListAsync();

      return bids.Count() == 0
        ? new Response<List<Bid>?>(null, 404, "No one bid not found.")
        : new Response<List<Bid>?>(bids.Select(b => new Bid
        {
          Id = b.Id,
          BidderId = b.BidderId,
          ItemFK = b.ItemFK,
          BidValue = b.BidValue,
          Bidder = b.Bidder
        }).ToList());
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
      var bid = await context
                            .Bids
                            .AsNoTracking()
                            .Select(b => new Bid
                            {
                              Id = b.Id,
                              BidderId = b.BidderId,
                              ItemFK = b.ItemFK,
                              BidValue = b.BidValue,
                              Bidder = b.Bidder
                            })
                            .FirstOrDefaultAsync(x => x.Id == request.Id);

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