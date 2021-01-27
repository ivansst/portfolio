using Portfolio.Server.Data.Models;
using Portfolio.Server.Models.Projects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Server.Services.Interfaces
{
  public interface IProjectService
  {
    Task<IEnumerable<ProjectsServiceModel>> GetAll();

    Task<ProjectsServiceModel> Get(int id);
  }
}
