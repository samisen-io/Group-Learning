using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupLearning.Models;

public class User
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int Id { get; set; }

  public string Uuid { get; set; } = $"usr-{Guid.NewGuid()}";

  public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

  public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;

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

  public DateTime? LastLoginDate { get; set; }

  [StringLength(15)]
  public string PhoneNumber { get; set; }

  public bool IsActive { get; set; } = true;

  [StringLength(100)]
  public string Role { get; set; } = "User";

  public virtual ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
  public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
  public virtual ICollection<File> Files { get; set; } = new List<File>();
}
