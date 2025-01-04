namespace Domer.Infrastructure.Services;
using DotNetEnv;


public class EnvService
{
    public static void Load()
    {
        // TODO find better path
        Env.Load("../../.env");
    }
}