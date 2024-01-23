using ProtoBuf;
using VoxInvasion.Server.Core.Servers;

namespace VoxInvasion.Server.Core.Packets.Ping;

[ProtoContract]
public class PongPacket : IPacket
{
    [ProtoMember(1)] public PacketId Id { get; } = PacketId.Pong;
    [ProtoMember(2)] public long ClientUnixMilliseconds { get; init; }

    public DateTimeOffset ClientTime
    {
        get => DateTimeOffset.FromUnixTimeMilliseconds(ClientUnixMilliseconds);
        init => ClientUnixMilliseconds = value.ToUnixTimeMilliseconds();
    }
}

public class PongHandler : IPacketHandler
{
    public PacketId Id { get; } = PacketId.Pong;

    public void Execute(IPacket packet, PlayerConnection connection)
    {
        var pongPacket = (PongPacket)packet;
        var currentServerTime = DateTimeOffset.UtcNow;
        var ping = (currentServerTime - pongPacket.ClientTime).Milliseconds;
        connection.SendAsync(new PingResultPacket { Ping = ping });
    }
}