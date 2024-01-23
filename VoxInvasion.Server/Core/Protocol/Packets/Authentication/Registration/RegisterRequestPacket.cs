using ProtoBuf;

namespace VoxInvasion.Server.Core.Protocol.Packets.Authentication.Registration;

[ProtoContract]
public class RegisterRequestPacket : IPacket
{
    [ProtoMember(1)] public PacketId Id { get; } = PacketId.RegisterRequest;
    [ProtoMember(2)] public string Username { get; init; } = null!;
    [ProtoMember(3)] public string Email { get; init; } = null!;
    [ProtoMember(4)] public string Password { get; init; } = null!;
}