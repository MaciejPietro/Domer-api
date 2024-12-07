using Ardalis.Result;
using Domer.Application.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Domer.Application.Heroes.CreateHero;

public class CreateHeroHandler : IRequestHandler<CreateHeroRequest, Result<GetHeroResponse>>
{
    private readonly IContext _context;
    
    
    public CreateHeroHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Result<GetHeroResponse>> Handle(CreateHeroRequest request, CancellationToken cancellationToken)
    {
        var created = Application.Heroes.Mapper.ToHeroEntity(request);
        _context.Heroes.Add(created);
        await _context.SaveChangesAsync(cancellationToken);
        return Application.Heroes.Mapper.ToHeroDto(created);
    }
}