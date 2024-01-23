using ProtoBuf;

namespace VoxInvasion.Server.Core.Packets.Ping;

[ProtoContract]
public class PingResultPacket : IPacket
{
    [ProtoMember(1)] public PacketId Id { get; } = PacketId.PingResult;
    [ProtoMember(2)] public int Ping { get; init; }
}