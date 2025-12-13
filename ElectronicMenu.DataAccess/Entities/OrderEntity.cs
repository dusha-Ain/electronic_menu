using System.ComponentModel.DataAnnotations.Schema;
using ElectronicMenuDataAccess.Entities;

namespace ElectronicMenuDataAccess.Entity;


[Table("Orders")] 
public class OrderEntity : BaseEntity
{
    
    public DateTime OrderDate { get; set; }
        
    [ForeignKey("FKStatus")]
    public int FKStatus { get; set; }
    public virtual StatusEntity StatusEntity { get; set; }
        
    [Column("TotalAmount")]
    public int TotalAmount { get; set; }
        
    [ForeignKey("FKUser")]
    public int FKUser { get; set; }
    public virtual UserEntity UserEntity  { get; set; }
        
    // Навигационные свойства
    public virtual ICollection<OrderItemEntity> OrderItems { get; set; }
    
    
    
}