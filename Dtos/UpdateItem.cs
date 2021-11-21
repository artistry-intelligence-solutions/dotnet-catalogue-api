using System.ComponentModel.DataAnnotations;

namespace Catalogue.Dtos
{
    public record UpdateItemDto
    {
        // Data anotations
        [Required]
        public string Name { get; init; }
        
        [Required]
        [Range(1, 1000)]
        public decimal Price { get; init; }
    }
}