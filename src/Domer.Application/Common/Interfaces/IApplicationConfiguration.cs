namespace Domer.Application.Common.Interfaces;

public interface IApplicationConfiguration
{
    string DbHost { get; }
    string DbDatabase { get; }
    string DbUsername { get; }
    string DbPassword { get; }
}
