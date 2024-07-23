using System.IO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupLearning.Models;

public class File
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int Id { get; set; }

  public string Uuid { get; set; } = $"file-{Guid.NewGuid()}";

  [Required]
  [StringLength(255)]
  public string FileName { get; set; }

  public string ContentType { get; set; }

  public long FileSize { get; set; }

  [Required]
  public byte[] FileContent { get; set; }

  public DateTime DateTime { get; set; } = DateTime.UtcNow;

  public DateTime UploadedOn { get; set; } = DateTime.UtcNow;

  [Required]
  [ForeignKey("User")]
  public int UserId { get; set; }

  public virtual User User { get; set; }

  [ForeignKey("Group")]
  public int? GroupId { get; set; }

  public virtual Group Group { get; set; }
}
