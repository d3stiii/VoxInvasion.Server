using ProtoBuf;

namespace VoxInvasion.Server.Core.Packets.Entrance.Login;

[ProtoContract]

public class LoginFromOtherConnectionPacket : IPacket
{
    [ProtoMember(1)] public PacketId Id { get; } = PacketId.LoginFromOtherConnection;
}