using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Server.Data.Models
{
  public class Project
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "NVARCHAR(1000)")]
    public string Title { get; set; }

    [Column(TypeName = "NVARCHAR(4000)")]
    public string ShortDescription { get; set; }

    [Column(TypeName = "NVARCHAR(max)")]
    public string Description { get; set; }

    [Column(TypeName = "NVARCHAR(500)")]
    public string Image { get; set; }
  }
}
