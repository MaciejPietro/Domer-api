using FluentValidation;
using Kompass.Application.Common.Validation;
using Kompass.Domain.Interfaces.Projects;


namespace Kompass.Application.Queries.Projects.GetProjectDevices;

public class GetProjectDevicesQueryValidator : AbstractValidator<GetProjectDevicesQuery>
{
    public GetProjectDevicesQueryValidator(IProjectRepository projectRepository)
    {
        RuleFor(x => x.Id).Cascade(CascadeMode.Stop).MustProjectExists(projectRepository);
    }
}