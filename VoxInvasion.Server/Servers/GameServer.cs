using System.Net;
using System.Net.Sockets;
using NetCoreServer;
using Serilog;
using VoxInvasion.Server.Utilities;

namespace VoxInvasion.Server.Servers;

public class GameServer(IPAddress host, ushort port) : TcpServer(host, port)
{
    public ILogger Logger { get; } = Log.Logger.ForType(typeof(GameServer));
    public List<PlayerConnection> Connections { get; } = new();

    protected override TcpSession CreateSession() => new PlayerConnection(this);

    protected override void OnConnected(TcpSession session) => Connections.Add((PlayerConnection)session);

    protected override void OnDisconnected(TcpSession session) => Connections.Add((PlayerConnection)session);

    protected override void OnStarted()
    {
        Logger.Information("Game server started");
    }

    protected override void OnError(SocketError error) => Logger.Error($"Game server caught an error: {error}");
}

public class PlayerConnection : TcpSession
{
    public PlayerConnection(TcpServer server) : base(server) { }
}