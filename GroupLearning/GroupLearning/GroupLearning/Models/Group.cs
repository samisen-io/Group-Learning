using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GroupLearning.Models;

public class Group
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int Id { get; set; }

  public string Uuid { get; set; } = $"grp-{Guid.NewGuid()}";

  [Required]
  [StringLength(100)]
  public string Name { get; set; }

  public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

  public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;

  [StringLength(500)]
  public string Description { get; set; }

  //[JsonIgnore]
  public virtual ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();

  //[JsonIgnore]
  public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

  //[JsonIgnore]
  public virtual ICollection<File> Files { get; set; } = new List<File>();
}
