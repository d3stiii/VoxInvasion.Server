using ProtoBuf;

namespace VoxInvasion.Server.Core.Protocol.Packets.Authentication.Login;

[ProtoContract]
public class LoginFailedPacket : IPacket
{ 
    [ProtoMember(1)] public PacketId Id { get; } = PacketId.LoginFailed;
}