using System.ComponentModel.DataAnnotations;

namespace GroupLearning.Models.RequestModel;

public class GroupRequestModel
{
  [StringLength(100)]
  public string Name { get; set; }

  [StringLength(500)]
  public string Description { get; set; }
}
