using Microsoft.AspNetCore.Mvc;
using Portfolio.Server.Models.Comments;
using Portfolio.Server.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Server.Controllers
{
  public class CommentsController : ApiController
  {
    private readonly ICommentService commentService;

    public CommentsController(ICommentService commentService)
    {
      this.commentService = commentService;
    }

    [HttpGet("{projectId}")]
    public async Task<IEnumerable<CommentsServiceModel>> GetAll(int projectId)
        => await this.commentService.GetAll(projectId);

    [HttpPost]
    public async Task<ActionResult> Create(CreateCommentRequestModel model)
    {
      var commentId = await this.commentService.Create(model.Content, this.UserId, model.ProjectId);

      return Created(nameof(this.Create), commentId);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> Update(int id, string content)
    {
      await this.commentService.Update(id, content, this.UserId);

      return Ok();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
      await this.commentService.Delete(id, this.UserId);

      return Ok();
    }
  }
}
