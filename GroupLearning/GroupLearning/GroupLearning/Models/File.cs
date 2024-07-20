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

  [Required]
  public File FileObject { get; set; }

  public DateTime DateTime { get; set; } = DateTime.UtcNow;

  public DateTime UploadedOn { get; set; } = DateTime.UtcNow;

  [Required]
  [ForeignKey("User")]
  public int UploadedById { get; set; }

  public User UploadedBy { get; set; }

  [ForeignKey("Group")]
  public int? GroupId { get; set; }

  public Group Group { get; set; }
}
