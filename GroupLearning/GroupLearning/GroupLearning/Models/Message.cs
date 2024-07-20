using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupLearning.Models;

public class Message
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int Id { get; set; }

  public string Uuid { get; set; } = $"msg-{Guid.NewGuid()}";

  [Required]
  public string Content { get; set; }

  public DateTime SentOn { get; set; } = DateTime.UtcNow;

  [Required]
  [ForeignKey("User")]
  public int UserId { get; set; }

  public User User { get; set; }

  [Required]
  [ForeignKey("Chat")]
  public int ChatId { get; set; }

  public Chat Chat { get; set; }
}
