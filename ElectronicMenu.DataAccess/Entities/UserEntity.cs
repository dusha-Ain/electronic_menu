using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ElectronicMenuDataAccess.Entities;

namespace ElectronicMenuDataAccess.Entity;


[Table("Users")] 
public class UserEntity : BaseEntity
{
    
    [MaxLength(100)]
    public string? Email { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string PasswordHash { get; set; } = string.Empty;
        
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;
        
    [Required]
    [MaxLength(50)]
    public string SecondName { get; set; } = string.Empty;
        
    [MaxLength(50)]
    public string? LastName { get; set; }
        
    public DateTime RegistrationDate { get; set; }
        
    [ForeignKey("FKRole")]
    public int FKRole { get; set; }
    public virtual UserRoleEntity UserRoleEntity  { get; set; } = null!;
        
    // Навигационные свойства
    public virtual ICollection<OrderEntity> Orders { get; set; }
}