using AutoMapper;
using FluentValidation;
using Kompass.Application.Common.Responses;
using Kompass.Application.Common.Validation;
using Kompass.Application.DTOs.Queries;
using Kompass.Domain.Entities.Projects;
using Kompass.Domain.Interfaces.Projects;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kompass.Application.Queries.Projects.GetProjectById;

public class GetProjectByIdQueryValidator : AbstractValidator<GetProjectByIdQuery>
{
    public GetProjectByIdQueryValidator()
    {
    }
}