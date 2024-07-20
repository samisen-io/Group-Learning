using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupLearning.Models;

public class Chat
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int Id { get; set; }

  public string Uuid { get; set; } = $"chat-{Guid.NewGuid()}";

  [Required]
  [ForeignKey("Group")]
  public int GroupId { get; set; }

  public Group Group { get; set; }

  public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
