using Microsoft.EntityFrameworkCore;
using Portfolio.Server.Data;
using Portfolio.Server.Data.Models;
using Portfolio.Server.Models.Comments;
using Portfolio.Server.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Server.Services
{
  public class CommentService : ICommentService
  {
    private readonly ApplicationDbContext data;

    public CommentService(ApplicationDbContext data)
    {
      this.data = data;
    }

    public async Task<IEnumerable<CommentsServiceModel>> GetAll(int projectId)
    {
      var comments = await this.data.Comments
                               .Where(c=> c.ProjectId == projectId)
                               .Select(c =>
                                   new CommentsServiceModel
                                   {
                                     Id = c.Id,
                                     Content = c.Content,
                                     ProjectId = c.ProjectId,
                                     User = this.data.Users
                                                     .FirstOrDefault(u=> u.Id == c.CreatedBy)
                                                     .UserName,
                                     CreatedBy = c.CreatedBy
                                   })
                               .ToListAsync();

      return comments;
    }

    public async Task<int> Create(string content, string userId, int projectId)
    {
      var project = await this.data.Projects.FirstOrDefaultAsync(p => p.Id == projectId);

      if(project == null)
      {
        throw new Exception("Can't create a comment for missing project");
      }

      var comment = new Comment
      {
        Content = content,
        CreatedBy = userId,
        ProjectId = projectId,
        CreationTime = DateTime.UtcNow
      };

      this.data.Add(comment);

      await this.data.SaveChangesAsync();

      return comment.Id;
    }

    public async Task Delete(int commentId, string userId)
    {
      var comment = await GetByIdAndUserId(commentId, userId);

      if(comment == null)
      {
        throw new Exception("Comment doesn't exist!");
      }

      if (comment.CreatedBy != userId)
      {
        throw new Exception("This user cannot delete this comment!");
      }

      comment.IsDeleted = true;

      await this.data.SaveChangesAsync();
    }

    public async Task Update(int commentId, string content, string userId)
    {
      var comment = await GetByIdAndUserId(commentId, userId);

      if (comment.CreatedBy != userId)
      {
        throw new Exception("This user cannot update this comment!");
      }

      comment.Content = content;

      await this.data.SaveChangesAsync();
    }

    private async Task<Comment> GetByIdAndUserId(int id, string userId)
    {
      var comment = await this.data
                              .Comments
                              .Where(c => c.Id == id && c.CreatedBy == userId)
                              .FirstOrDefaultAsync();

      return comment;
    }
  }
}
