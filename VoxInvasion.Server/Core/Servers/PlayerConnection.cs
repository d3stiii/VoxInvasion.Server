using System.Net.Sockets;
using NetCoreServer;
using Serilog;
using VoxInvasion.Server.Core.Database.Models;
using VoxInvasion.Server.Core.Packets;
using VoxInvasion.Server.Core.Packets.Entrance;
using VoxInvasion.Server.Core.Utilities;

namespace VoxInvasion.Server.Core.Servers;

public class PlayerConnection(GameServer server, PacketHandlersProvider packetHandlersProvider)
    : TcpSession(server)
{
    public ILogger Logger = Log.Logger.ForType(typeof(PlayerConnection));

    public bool IsSocketConnected => IsConnected && !IsDisposed && !IsSocketDisposed;
    public GameServer ConnectedServer { get; } = server;
    public Player PlayerData { get; set; } = null!;
    public bool LoggedIn => IsSocketConnected && PlayerData != null!;

    public void SendAsync(IPacket packet)
    {
        byte[] buffer = packet.Serialize();
        Logger.Verbose($"Outgoing packet: {packet.Id}");
        base.SendAsync(buffer);
    }

    protected override void OnConnected()
    {
        Logger = Logger.WithConnection(this);
        Logger.Information($"Connection with id {Id} established");
        SendAsync(new WelcomePacket { Message = "VoxInvasion へようこそ" });
    }

    protected override void OnReceived(byte[] buffer, long offset, long size)
    {
        IPacket packet = buffer.Deserialize(offset, size);
        Logger.Verbose($"Incoming packet: {packet.Id}");
        var handler = packetHandlersProvider.GetHandler(packet.Id);
        if (handler == null)
        {
            Logger.Warning($"No handler found for packet {packet.Id}");
            return;
        }

        handler.Execute(packet, this);
    }

    protected override void OnDisconnected() =>
        Logger.Information($"Connection with id {Id} closed");

    protected override void OnError(SocketError error) =>
        Logger.Error("An error has occurred with code {Error}", error);
}