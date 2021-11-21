using System.ComponentModel.DataAnnotations;

namespace Catalogue.Dtos
{
    public record CreateItemDto
    {
        // Data anotations
        [Required]
        public string Name { get; init; }
        
        [Required]
        [Range(1, 1000)]
        public decimal Price { get; init; }
    }
}