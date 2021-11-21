using System;
using System.Collections.Generic;
using Catalogue.entities;

namespace Catalogue.Repositories
{
  /// <summary>
  /// methods that will be used at http endpoints
  /// </summary>
	public interface IItemsRepository
	{
    // For GET
		Item GetItem(Guid id);

		IEnumerable<Item> GetItems();

    // For POST
    void CreateItem(Item item);

    void UpdateItem(Item item);
    void DeleteItem(Guid id);
  }
}