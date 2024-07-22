using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupLearning.Models
{
  public class UserGroup
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }
    public virtual User User { get; set; }

    [ForeignKey("Group")]
    public int GroupId { get; set; }
    public virtual Group Group { get; set; }
  }
}
