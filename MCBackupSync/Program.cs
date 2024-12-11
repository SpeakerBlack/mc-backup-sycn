

using MCBackupSync;

if (args.Length == 0)
{
    Environment.Exit(0);
}

Startup.Build(args[0]);
