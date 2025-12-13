using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ElectronicMenuDataAccess.Entities;

namespace ElectronicMenuDataAccess.Entity;

[Table("Dishes")] 
public class DishEntity : BaseEntity
{
    [Required]
    [MaxLength(30)]
    public string DishName { get; set; } = string.Empty;
        
    public string? Description { get; set; }
    
    public decimal Price { get; set; }
        
    [ForeignKey("FKCategory")]
    public int FKCategory { get; set; }
    public virtual CategoryDishEntity CategoryDishEntity { get; set; }
        
    // Навигационные свойства
    public virtual ICollection<OrderItemEntity> OrderItems { get; set; }
}