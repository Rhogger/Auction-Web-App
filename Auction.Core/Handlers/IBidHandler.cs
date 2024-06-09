using Auction.Core.Models;
using Auction.Core.Requests;
using Auction.Core.Requests.Bids;
using Auction.Core.Responses;

namespace Auction.Core.Handlers;

public interface IBidHandler
{
  Task<Response<Bid?>> CreateAsync(CreateBidRequest request);
  Task<Response<Bid?>> UpdateAsync(UpdateBidRequest request);
  Task<Response<Bid?>> DeleteAsync(DeleteRequest request);
  Task<Response<Bid?>> GetByIdAsync(GetByIdRequest request);
  Task<Response<List<Bid>?>> GetAllAsync(GetAllBidRequest request);
}