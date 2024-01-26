using ProtoBuf;

namespace VoxInvasion.Server.Core.Packets.Entrance.Validation;

[ProtoContract]
public class UsernameValidPacket : IPacket
{
    [ProtoMember(1)] public PacketId Id { get; } = PacketId.UsernameValid;
}