using Auction.Core.Models;
using Auction.Core.Requests;
using Auction.Core.Requests.Bidders;
using Auction.Core.Requests.Items;
using Auction.Core.Responses;

namespace Auction.Core.Handlers;

public interface IBidderHandler
{
  Task<Response<Bidder?>> CreateAsync(CreateBidderRequest request);
  Task<Response<Bidder?>> UpdateAsync(UpdateBidderRequest request);
  Task<Response<Bidder?>> DeleteAsync(DeleteRequest request);
  Task<Response<Bidder?>> GetByIdAsync(GetByIdRequest request);
  Task<PagedResponse<List<Bidder>?>> GetAllPagedAsync(GetAllPagedRequest request);
  Task<Response<List<Bidder>?>> GetAllAsync(GetAllRequest request);
}