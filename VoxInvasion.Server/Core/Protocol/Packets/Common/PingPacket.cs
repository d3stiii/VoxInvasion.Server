using ProtoBuf;

namespace VoxInvasion.Server.Core.Protocol.Packets.Common;

[ProtoContract]
public class PingPacket : IPacket
{
    [ProtoMember(1)] public PacketId Id { get; } = PacketId.Ping;
    [ProtoMember(2)] public long UnixMilliseconds { get; init; }

    public DateTimeOffset CurrentTime
    {
        get => DateTimeOffset.FromUnixTimeMilliseconds(UnixMilliseconds);
        init => UnixMilliseconds = value.ToUnixTimeMilliseconds();
    }
}