using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ElectronicMenuDataAccess.Entities;

namespace ElectronicMenuDataAccess.Entity;


[Table("OrderItems")] 
public class OrderItemEntity : BaseEntity
{
    [ForeignKey("FKOrder")]
    public int FKOrder { get; set; }
    public virtual OrderEntity OrderEntity { get; set; } = null!;
        
    [ForeignKey("FKDish")]
    public int FKDish { get; set; }
    public virtual DishEntity DishEntity { get; set; } = null!;
        
    [Required]
    public int Quantity { get; set; }
        
    [Required]
    [Column("UnitPrice")]
    public int UnitPrice { get; set; }
}