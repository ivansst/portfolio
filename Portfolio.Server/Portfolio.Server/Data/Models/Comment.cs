using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Server.Data.Models
{
  public class Comment
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "ntext")]
    public string Content { get; set; }

    [Required]
    public string CreatedBy { get; set; }

    [ForeignKey("CreatedBy")]
    public User User { get; set; }

    [Column(TypeName = "DATETIMEOFFSET")]
    public DateTimeOffset CreationTime { get; set; }

    [Required]
    [Column(TypeName = "BIT")]
    public bool IsDeleted { get; set; }

    [Required]
    public int ProjectId { get; set; }

    public Project Project { get; set; }
  }
}
