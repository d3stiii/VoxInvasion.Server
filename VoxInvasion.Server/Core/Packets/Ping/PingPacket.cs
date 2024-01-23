using ProtoBuf;

namespace VoxInvasion.Server.Core.Packets.Ping;

[ProtoContract]
public class PingPacket : IPacket
{
    [ProtoMember(1)] public PacketId Id { get; } = PacketId.Ping;
    [ProtoMember(2)] public long UnixMilliseconds { get; init; }
}