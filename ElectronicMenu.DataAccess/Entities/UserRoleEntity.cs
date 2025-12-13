using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ElectronicMenuDataAccess.Entities;

namespace ElectronicMenuDataAccess.Entity;


[Table("Roles")] 
public class UserRoleEntity : BaseEntity
{
      
    [Required]
    [MaxLength(12)]
    public string RoleName { get; set; } = string.Empty;
        
    // Навигационные свойства
    public virtual ICollection<UserEntity> Users { get; set; }
}