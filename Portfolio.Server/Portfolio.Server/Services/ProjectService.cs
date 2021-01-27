using Microsoft.EntityFrameworkCore;
using Portfolio.Server.Data;
using Portfolio.Server.Data.Models;
using Portfolio.Server.Models.Projects;
using Portfolio.Server.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Server.Services
{
  public class ProjectService : IProjectService
  {
    private readonly ApplicationDbContext data;

    public ProjectService(ApplicationDbContext data)
    {
      this.data = data;
    }

    public async Task<ProjectsServiceModel> Get(int id)
    {
      var project = await data.Projects
                              .FirstOrDefaultAsync(p => p.Id == id);

      if (project == null)
      {
        throw new Exception("There is no project corresponding to this Id.");
      }

      var model = new ProjectsServiceModel
      {
        Id = project.Id,
        Title = project.Title,
        Description = project.Description,
        Image = project.Image
      };

      return model;
    }

    public async Task<IEnumerable<ProjectsServiceModel>> GetAll()
    {
      var projects = await data.Projects
                               .Select(p =>
                                    new ProjectsServiceModel
                                    {
                                      Id = p.Id,
                                      Title = p.Title,
                                      Description = p.ShortDescription,
                                      Image = p.Image
                                    })
                               .ToListAsync();

      return projects;
    }

    private async Task Create(string title)
    {
      var project = new Project
      {
        Title = title
      };

      this.data.Add(project);

      await this.data.SaveChangesAsync();
    }
  }
}
