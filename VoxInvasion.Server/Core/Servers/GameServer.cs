using System.Net;
using System.Net.Sockets;
using NetCoreServer;
using Serilog;
using VoxInvasion.Server.Core.Database;
using VoxInvasion.Server.Core.Protocol.Packets;
using VoxInvasion.Server.Core.Protocol.Packets.Common;
using VoxInvasion.Server.Core.Utilities;

namespace VoxInvasion.Server.Core.Servers;

public class GameServer(IPAddress host, ushort port) : TcpServer(host, port)
{
    private static readonly ILogger Logger = Log.Logger.ForType(typeof(GameServer));
    private readonly PacketHandlersProvider _packetHandlersProvider = new();

    public DatabaseContext Context { get; } = new();
    public List<PlayerConnection> Connections { get; } = new();

    protected override void OnStarted()
    {
        _packetHandlersProvider.Initialize();
        Logger.Information("Game server started");

        new Thread(PingLoop) { Name = "Ping loop" }.Start();
    }

    private void PingLoop()
    {
        while (true)
        {
            if (!IsStarted) return;

            foreach (var connection in Connections.ToArray())
            {
                try
                {
                    connection.SendAsync(new PingPacket { CurrentTime = DateTimeOffset.UtcNow });
                }
                catch (Exception e)
                {
                    Logger.Error(e, "Socket caught an exception while sending ping packet");
                }
            }

            Thread.Sleep(10000);
        }
    }

    protected override void OnStopped() => _packetHandlersProvider.Clear();
    protected override TcpSession CreateSession() => new PlayerConnection(this, _packetHandlersProvider);
    protected override void OnConnected(TcpSession session) => Connections.Add((PlayerConnection)session);
    protected override void OnDisconnected(TcpSession session) => Connections.Remove((PlayerConnection)session);
    protected override void OnError(SocketError error) => Logger.Error($"Game server caught an error: {error}");
}