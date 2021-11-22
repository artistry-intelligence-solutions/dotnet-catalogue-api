using System;
using System.Collections.Generic;
using System.Linq;
using Catalogue.entities;
using Catalogue.Repositories;
using Catalogue.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Catalogue.Controllers
{
	
	/// <summary>
	/// : ControllerBase signifies inheritance
	/// </summary>
	[ApiController]
	[Route("items")]
	public class ItemsController : ControllerBase
	{
		/// <summary>
		/// Declaring an instance or a depedancy
		/// </summary>
		private readonly IItemsRepository repository;

		/// <summary>
		/// Items constructor
		/// </summary>
		public ItemsController(IItemsRepository repository)
		{
      this.repository = repository;
    }

		/// <summary>
		/// Get method will access this
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<IEnumerable<ItemDto>> GetItemsAsync()
		{
			var items = (await repository.GetItemsAsync()).Select(item => item.AsDto());
			return items;
		}

		/// <summary>
		/// will be GET /items/{id}
		/// </summary>
		/// <param name="id">id</param>
		/// <returns>item associated with given id</returns>
		[HttpGet("{id}")]
		public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
		{
			var item = await repository.GetItemAsync(id);

			if (item == null)
			{
				// tell dotnet to correctly handle the Http status code in case of error
				// doesn't return an error because multiple types allowed > ActionResult
				return NotFound();
			}

			// return ok(item);
			return item.AsDto();
		}

    /// <summary>
    /// CreateItemDto represents the data type object or contract the user makes when making a request
    /// Post /items
    /// </summary>
    /// <param name="itemDto">ID</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
    {
      Item item = new()
      {
        Id = Guid.NewGuid(),
        Name = itemDto.Name, 
        Price = itemDto.Price,
        CreatedDate = DateTimeOffset.UtcNow
      };

      await repository.CreateItemAsync(item);

      // we need to specify the header information and how to access it ie >>  GetItem(), the parameter and the final object
      return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item.AsDto());
    }

    // PUT /items/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto itemDto)
    {
      var existingItem = await repository.GetItemAsync(id);

      if (existingItem is null)
      {
        return NotFound();
      }

      Item updatedItem = existingItem with
      {
        Name = itemDto.Name,
        Price = itemDto.Price
      };

      await repository.UpdateItemAsync(updatedItem);

      return NoContent();
    }

    // DELETE /items/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteItemAsync(Guid id)
    {
      var existingItem = await repository.GetItemAsync(id);

      if (existingItem is null)
      {
        return NotFound();
      }

      await repository.DeleteItemAsync(id);

      return NoContent();
    }
	}
}