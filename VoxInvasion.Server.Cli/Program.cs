﻿using System.Net;
using Serilog.Events;
using VoxInvasion.Server.Servers;
using VoxInvasion.Server.Utilities;

namespace VoxInvasion.Server.Cli;

public static class Program
{
    public static Task Main(string[] args)
    {
        Logger.Initialize(LogEventLevel.Information);
        GameServer gameServer = new(IPAddress.Any, 25565);
        new Thread(() => gameServer.Start()) { Name = "Game Server" }.Start();

        return Task.Delay(-1);
    }
}