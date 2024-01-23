using ProtoBuf;

namespace VoxInvasion.Server.Core.Protocol.Packets.Authentication.Login;

[ProtoContract]
public class LoginSuccessPacket : IPacket
{
    [ProtoMember(1)] public PacketId Id { get; } = PacketId.LoginSuccess;
}