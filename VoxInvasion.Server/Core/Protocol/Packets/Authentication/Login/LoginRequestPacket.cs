using ProtoBuf;

namespace VoxInvasion.Server.Core.Protocol.Packets.Authentication.Login;

[ProtoContract]
public class LoginRequestPacket : IPacket
{
    [ProtoMember(1)] public PacketId Id { get; } = PacketId.LoginRequest;
    [ProtoMember(2)] public string Username { get; init; } = null!;
    [ProtoMember(3)] public string Password { get; init; } = null!;
}