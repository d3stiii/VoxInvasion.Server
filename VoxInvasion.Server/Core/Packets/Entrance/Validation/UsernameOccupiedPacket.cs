using ProtoBuf;

namespace VoxInvasion.Server.Core.Packets.Entrance.Validation;

[ProtoContract]
public class UsernameOccupiedPacket : IPacket
{
    [ProtoMember(1)] public PacketId Id { get; } = PacketId.UsernameOccupied;
}