using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ElectronicMenuDataAccess.Entities;

namespace ElectronicMenuDataAccess.Entity;

[Table("CategoriesDishes")] 
public class CategoryDishEntity : BaseEntity
{
    [Required]
    [MaxLength(20)]
    public string CategoryName { get; set; } = string.Empty;
        
    // Навигационные свойства
    public virtual ICollection<DishEntity> Dishes { get; set; }
}