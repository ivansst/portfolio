namespace Portfolio.Server.Models.Comments
{
  public class CommentsServiceModel
  {
    public int Id { get; set; }

    public int ProjectId { get; set; }

    public string Content { get; set; }

    public string User { get; set; }

    public string CreatedBy { get; set; }
  }
}