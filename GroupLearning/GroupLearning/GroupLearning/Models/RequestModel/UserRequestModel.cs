using System.ComponentModel.DataAnnotations;

namespace GroupLearning.Models.RequestModels;

public class UserRequestModel
{
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
  public string Password { get; set; }

  [StringLength(15)]
  public string PhoneNumber { get; set; }
}
