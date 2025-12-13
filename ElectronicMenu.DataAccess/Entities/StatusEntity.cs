using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ElectronicMenuDataAccess.Entities;

namespace ElectronicMenuDataAccess.Entity;


[Table("Statuses")] 
public class StatusEntity : BaseEntity
{
    [Required]
    [MaxLength(12)]
    public string StatusName { get; set; } = string.Empty;
        
    // Навигационные свойства
    public virtual ICollection<OrderEntity> Orders { get; set; }
}