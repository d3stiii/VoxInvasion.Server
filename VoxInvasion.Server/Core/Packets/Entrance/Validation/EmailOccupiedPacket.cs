using ProtoBuf;

namespace VoxInvasion.Server.Core.Packets.Entrance.Validation;

[ProtoContract]
public class EmailOccupiedPacket : IPacket
{
    [ProtoMember(1)] public PacketId Id { get; } = PacketId.EmailOccupied;
}