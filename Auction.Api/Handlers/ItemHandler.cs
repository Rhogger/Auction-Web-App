using Auction.Api.Data;
using Auction.Core.Handlers;
using Auction.Core.Models;
using Auction.Core.Requests;
using Auction.Core.Requests.Bids;
using Auction.Core.Requests.Items;
using Auction.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Auction.Api.Handlers;

public class ItemHandler(AppDbContext context) : IItemHandler
{
  public async Task<Response<Item?>> CreateAsync(CreateItemRequest request)
  {
    try
    {
      var item = new Item
      {
        Name = request.Name,
        Description = request.Description,
        InicialBidValue = request.InicialBidValue,
        TimeEndAuction = request.TimeEndAuction,
      };

      await context.Items.AddAsync(item);
      await context.SaveChangesAsync();
      return new Response<Item?>(item, 201, "Item succesfully created.");
    }
    catch
    {
      return new Response<Item?>(null, 500, "Item could not be created.");
    }
  }

  public async Task<Response<Item?>> UpdateAsync(UpdateItemRequest request)
  {
    try
    {
      var item = await context.Items.FirstOrDefaultAsync(x => x.Id == request.Id);

      if (item == null)
        return new Response<Item?>(null, 404, "Item not found.");

      item.Name = request.Name;
      item.Description = request.Description;
      item.InicialBidValue = request.InicialBidValue;
      item.TimeEndAuction = request.TimeEndAuction;

      if (item.TimeEndAuction <= item.CreatedAt.AddHours(1))
        return new Response<Item?>(null, 400, "The item cannot be updated because the item's auction end time is less than the allowed time.");

      context.Items.Update(item);
      await context.SaveChangesAsync();

      return new Response<Item?>(item, message: "Item succesfully updated.");
    }
    catch
    {
      return new Response<Item?>(null, 500, "Item could not be updated.");
    }
  }

  public async Task<Response<Item?>> DeleteAsync(DeleteRequest request)
  {
    try
    {
      var item = await context.Items.FirstOrDefaultAsync(x => x.Id == request.Id);

      if (item == null)
        return new Response<Item?>(null, 404, "Item not found.");

      context.Items.Remove(item);
      await context.SaveChangesAsync();

      return new Response<Item?>(item, message: "Item succesfully removed.");
    }
    catch
    {
      return new Response<Item?>(null, 500, "Item could not be removed.");
    }
  }

  public async Task<PagedResponse<List<Item>?>> GetAllPagedAsync(GetAllPagedRequest request)
  {
    try
    {
      var query = context
                        .Items
                        .AsNoTracking()
                        .OrderBy(x => x.TimeEndAuction)
                        .OrderBy(x => x.CreatedAt)
                        .Select(item => new Item
                        {
                          Id = item.Id,
                          Name = item.Name,
                          Description = item.Description,
                          InicialBidValue = item.InicialBidValue,
                          CreatedAt = item.CreatedAt,
                          TimeEndAuction = item.TimeEndAuction,
                          Bids = item
                                      .Bids
                                      .Select(b => new Bid
                                      {
                                        Id = b.Id,
                                        BidderId = b.BidderId,
                                        Bidder = b.Bidder,
                                        ItemFK = b.ItemFK,
                                        BidValue = b.BidValue,
                                      })
                                      .ToList(),
                          HighestBid = item.Bids.Any()
                                                      ? item
                                                            .Bids
                                                            .Where(b => b.BidValue == item
                                                                                          .Bids
                                                                                          .Max(b => b.BidValue))
                                                            .Select(b => new Bid
                                                            {
                                                              Id = b.Id,
                                                              BidderId = b.BidderId,
                                                              BidValue = b.BidValue,
                                                              Bidder = b.Bidder
                                                            })
                                                            .FirstOrDefault()
                                                      : null
                        });

      var items = await query
                            .Skip((request.PageNumber - 1) * request.PageSize)
                            .Take(request.PageSize)
                            .ToListAsync();

      var count = await query.CountAsync();

      return new PagedResponse<List<Item>?>(items, count, request.PageNumber, request.PageSize);
    }
    catch
    {
      return new PagedResponse<List<Item>?>(null, 500, "Items could not be found.");
    }
  }

  public async Task<Response<Item?>> GetByIdAsync(GetByIdRequest request)
  {
    try
    {
      var item = await context
                              .Items
                              .AsNoTracking()
                              .Where(x => x.Id == request.Id)
                              .Select(item => new Item
                              {
                                Id = item.Id,
                                Name = item.Name,
                                Description = item.Description,
                                InicialBidValue = item.InicialBidValue,
                                CreatedAt = item.CreatedAt,
                                TimeEndAuction = item.TimeEndAuction,
                                Bids = item
                                          .Bids
                                          .Select(b => new Bid
                                          {
                                            Id = b.Id,
                                            BidderId = b.BidderId,
                                            Bidder = b.Bidder,
                                            ItemFK = b.ItemFK,
                                            BidValue = b.BidValue,
                                          })
                                          .ToList(),
                                HighestBid = item
                                                  .Bids
                                                  .Any()
                                                        ? item
                                                              .Bids
                                                              .Where(b => b.BidValue == item
                                                                                            .Bids
                                                                                            .Max(b => b.BidValue))
                                                              .Select(b => new Bid
                                                              {
                                                                Id = b.Id,
                                                                BidderId = b.BidderId,
                                                                BidValue = b.BidValue,
                                                                Bidder = b.Bidder
                                                              })
                                                              .FirstOrDefault()
                                                        : null
                              })
                              .FirstOrDefaultAsync();

      return item is null
        ? new Response<Item?>(null, 404, "Item not found.")
        : new Response<Item?>(item);
    }
    catch
    {
      return new Response<Item?>(null, 500, "Item could not be found.");
    }
  }
}