using Kompass.Domain.Common;
using Kompass.Domain.Entities.Devices;
using Kompass.Domain.Interfaces.Devices;
using Kompass.Domain.Interfaces.Projects;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Application.Services.Projects;

public interface IProjectService
{

    Task<(IProject? project, IProjectDetails? projectDetails, IProjectCreator? projectCreator)> GetProjectWithDetailsAsync(
        ProjectId projectId,
        CancellationToken cancellationToken);
}