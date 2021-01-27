using Portfolio.Server.Models.Comments;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Server.Services.Interfaces
{
  public interface ICommentService
  {
    public Task<IEnumerable<CommentsServiceModel>> GetAll(int projectId);

    public Task<int> Create(string content, string userId, int projectId);

    public Task Update(int commentId, string content, string userId);

    public Task Delete(int commentId, string userId);
  }
}
