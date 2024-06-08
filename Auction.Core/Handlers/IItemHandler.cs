using Auction.Core.Models;
using Auction.Core.Requests.Items;
using Auction.Core.Responses;

namespace Auction.Core.Handlers;

public interface IItemHandler
{
  Task<Response<Item?>> CreateAsync(CreateItemRequest request);
  Task<Response<Item?>> UpdateAsync(UpdateItemRequest request);
  Task<Response<Item?>> DeleteAsync(DeleteItemRequest request);
  Task<Response<Item?>> GetByIdAsync(GetItemByIdRequest request);
  Task<PagedResponse<List<Item>?>> GetAllAsync(GetAllItemsRequest request);
}