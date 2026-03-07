using System;
using System.IO;

namespace Kompass.Infrastructure.Services;
using DotNetEnv;
using System.Reflection;


public abstract class EnvService
{
    public static void Load()
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        string envPath = Path.Combine(currentDirectory, ".env");

        // If not found in current directory, search up the directory tree
        if (!File.Exists(envPath))
        {
            DirectoryInfo? directoryInfo = new DirectoryInfo(currentDirectory);
            while (directoryInfo != null)
            {
                string potentialPath = Path.Combine(directoryInfo.FullName, ".env");
                if (File.Exists(potentialPath))
                {
                    envPath = potentialPath;
                    break;
                }
                directoryInfo = directoryInfo.Parent;
            }
        }

        if (File.Exists(envPath))
        {
            Env.Load(envPath);
        }
    }
}