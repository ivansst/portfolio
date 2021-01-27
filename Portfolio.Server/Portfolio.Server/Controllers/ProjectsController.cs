using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Server.Models.Projects;
using Portfolio.Server.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Server.Controllers
{
  public class ProjectsController : ApiController
  {
    private readonly IProjectService projectsService;

    public ProjectsController(IProjectService projectsService)
    {
      this.projectsService = projectsService;
    }

    [AllowAnonymous]
    [HttpGet]
    [Route(nameof(GetAll))]
    public async Task<IEnumerable<ProjectsServiceModel>> GetAll()
        => await this.projectsService.GetAll();


    [HttpGet]
    [Route("{id}")]
    public async Task<ProjectsServiceModel> Get(int id)
        => await this.projectsService.Get(id);
  }
}
