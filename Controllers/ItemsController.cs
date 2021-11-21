using System;
using System.Collections.Generic;
using System.Linq;
using Catalogue.entities;
using Catalogue.Repositories;
using Catalogue.Dtos;
using Microsoft.AspNetCore.Mvc;

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
		public IEnumerable<ItemDto> GetItems()
		{
			var items = repository.GetItems().Select(item => item.AsDto());
			return items;
		}

		/// <summary>
		/// will be GET /items/{id}
		/// </summary>
		/// <param name="id">id</param>
		/// <returns>item associated with given id</returns>
		[HttpGet("{id}")]
		public ActionResult<ItemDto> GetItem(Guid id)
		{
			var item = repository.GetItem(id);

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
    public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
    {
      Item item = new()
      {
        Id = Guid.NewGuid(),
        Name = itemDto.Name, 
        Price = itemDto.Price,
        CreatedDate = DateTimeOffset.UtcNow
      };

      repository.CreateItem(item);

      // we need to specify the header information and how to access it ie >>  GetItem(), the parameter and the final object
      return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item.AsDto());
    }

    // PUT /items/{id}
    [HttpPut("{id}")]
    public ActionResult UpdateItem(Guid id, UpdateItemDto itemDto)
    {
      var existingItem = repository.GetItem(id);

      if (existingItem is null)
      {
        return NotFound();
      }

      Item updatedItem = existingItem with
      {
        Name = itemDto.Name,
        Price = itemDto.Price
      };

      repository.UpdateItem(updatedItem);

      return NoContent();
    }

    // DELETE /items/{id}
    [HttpDelete("{id}")]
    public ActionResult DeleteItem(Guid id)
    {
      var existingItem = repository.GetItem(id);

      if (existingItem is null)
      {
        return NotFound();
      }

      repository.DeleteItem(id);

      return NoContent();
    }
	}
}