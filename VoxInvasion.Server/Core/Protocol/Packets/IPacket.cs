using ProtoBuf;
using VoxInvasion.Server.Core.Protocol.Packets.Authentication.Login;
using VoxInvasion.Server.Core.Protocol.Packets.Authentication.Registration;
using VoxInvasion.Server.Core.Protocol.Packets.Common;

namespace VoxInvasion.Server.Core.Protocol.Packets;

[ProtoContract]
[ProtoInclude(100, typeof(WelcomePacket))]
[ProtoInclude(200, typeof(PingPacket))]
[ProtoInclude(300, typeof(LoginRequestPacket))]
[ProtoInclude(400, typeof(RegisterRequestPacket))]
[ProtoInclude(500, typeof(RegistrationFailedPacket))]
[ProtoInclude(600, typeof(LoginSuccessPacket))]
[ProtoInclude(700, typeof(LoginFailedPacket))]
public interface IPacket
{
    PacketId Id { get; }
}