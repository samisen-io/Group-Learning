using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupLearning.Models.RequestModel;

public class FileRequestModel
{
  public IFormFile UploadFile { get; set; }

  public int UserId { get; set; }

  public int GroupId { get; set; }
}
