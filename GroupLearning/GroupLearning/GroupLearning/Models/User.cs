using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupLearning.Models;

public class User
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int Id { get; set; }

  [Required]
  [StringLength(50)]
  public string FirstName { get; set; }

  [Required]
  [StringLength(50)]
  public string LastName { get; set; }

  [Required]
  [StringLength(100)]
  [EmailAddress]
  public string Email { get; set; }

  [Required]
  public string PasswordHash { get; set; }

  public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

  public DateTime? LastLoginDate { get; set; }

  [StringLength(15)]
  public string PhoneNumber { get; set; }

  public bool IsActive { get; set; } = true;

  [StringLength(100)]
  public string Role { get; set; } = "User";
}
