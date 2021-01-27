using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Server.Models.Comments
{
  public class CreateCommentRequestModel
  {
    [Required]
    public string Content { get; set; }

    [Required]
    public int ProjectId { get; set; }
  }
}
