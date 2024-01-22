using System.Net;
using System.Net.Sockets;
using NetCoreServer;
using Serilog;
using VoxInvasion.Server.Core.Protocol.Packets;
using VoxInvasion.Server.Core.Protocol.Packets.Common;
using VoxInvasion.Server.Core.Utilities;

namespace VoxInvasion.Server.Core.Servers;

public class PlayerConnection(TcpServer server, PacketHandlersProvider packetHandlersProvider)
    : TcpSession(server)
{
    private static readonly ILogger Logger = Log.Logger.ForType(typeof(PlayerConnection));

    public void SendAsync(IPacket packet)
    {
        var buffer = packet.Serialize();
        Logger.Verbose($"Outgoing packet: {packet.Id}");
        base.SendAsync(buffer);
    }

    protected override void OnConnected()
    {
        Logger.Information($"({(IPEndPoint)Socket.RemoteEndPoint!} player connection with id {Id} established)");
        SendAsync(new WelcomePacket { Message = "VoxInvasion へようこそ" });
    }

    protected override void OnReceived(byte[] buffer, long offset, long size)
    {
        var packet = buffer.Deserialize(offset, size);
        var handler = packetHandlersProvider.GetHandler(packet.Id);
        if (handler == null)
        {
            Logger.Warning($"No handler found for packet {packet.Id}");
            return;
        }

        handler.Execute(packet, this);
    }

    protected override void OnDisconnected()
    {
        Logger.Information($"({(IPEndPoint)Socket.RemoteEndPoint!} player connection with id {Id} ended)");
    }

    protected override void OnError(SocketError error) =>
        Logger.Error("An error has occurred with code {Error}", error);
}