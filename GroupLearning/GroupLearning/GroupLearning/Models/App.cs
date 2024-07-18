using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GroupLearning.Models;

public class App
{
  [Key]
  public int Id { get; set; }
  [Required]
  public string Name { get; set; } = string.Empty;
  [DisplayName("Table Ready")]
  public bool TableReady { get; set; }
  [Required]
  public string Description { get; set; } = string.Empty;
}
