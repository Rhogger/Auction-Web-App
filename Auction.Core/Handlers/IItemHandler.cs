using Auction.Core.Models;
using Auction.Core.Requests;
using Auction.Core.Requests.Items;
using Auction.Core.Responses;

namespace Auction.Core.Handlers;

public interface IItemHandler
{
  Task<Response<Item?>> CreateAsync(CreateItemRequest request);
  Task<Response<Item?>> UpdateAsync(UpdateItemRequest request);
  Task<Response<Item?>> DeleteAsync(DeleteRequest request);
  Task<Response<Item?>> GetByIdAsync(GetByIdRequest request);
  Task<PagedResponse<List<Item>?>> GetAllPagedAsync(GetAllPagedRequest request);
}