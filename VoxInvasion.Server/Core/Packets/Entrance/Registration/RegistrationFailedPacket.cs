using ProtoBuf;

namespace VoxInvasion.Server.Core.Packets.Entrance.Registration;

[ProtoContract]
public class RegistrationFailedPacket : IPacket
{
    [ProtoMember(1)] public PacketId Id { get; } = PacketId.RegistrationFailed;
}