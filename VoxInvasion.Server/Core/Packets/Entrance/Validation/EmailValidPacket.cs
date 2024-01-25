using ProtoBuf;

namespace VoxInvasion.Server.Core.Packets.Entrance.Validation;

[ProtoContract]
public class EmailValidPacket : IPacket
{
    [ProtoMember(1)] public PacketId Id { get; } = PacketId.EmailValid;
}