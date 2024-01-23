using ProtoBuf;
using VoxInvasion.Server.Core.Packets.Entrance;
using VoxInvasion.Server.Core.Packets.Entrance.Login;
using VoxInvasion.Server.Core.Packets.Entrance.Registration;
using VoxInvasion.Server.Core.Packets.Ping;

namespace VoxInvasion.Server.Core.Packets;

[ProtoContract]
[ProtoInclude(100, typeof(WelcomePacket))]
[ProtoInclude(200, typeof(PingPacket))]
[ProtoInclude(300, typeof(LoginRequestPacket))]
[ProtoInclude(400, typeof(RegisterRequestPacket))]
[ProtoInclude(500, typeof(RegistrationFailedPacket))]
[ProtoInclude(600, typeof(LoginSuccessPacket))]
[ProtoInclude(700, typeof(LoginFailedPacket))]
[ProtoInclude(800, typeof(PongPacket))]
[ProtoInclude(900, typeof(PingResultPacket))]
public interface IPacket
{
    PacketId Id { get; }
}