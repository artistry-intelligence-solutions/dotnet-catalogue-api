using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalogue.entities;

namespace Catalogue.Repositories
{
  /// <summary>
  /// methods that will be used at http endpoints
  /// </summary>
	public interface IItemsRepository
	{
    // For GET
		Task<Item> GetItemAsync(Guid id);

		Task<IEnumerable<Item>> GetItemsAsync();

    // For POST
    Task CreateItemAsync(Item item);

    Task UpdateItemAsync(Item item);
    Task DeleteItemAsync(Guid id);
  }
}