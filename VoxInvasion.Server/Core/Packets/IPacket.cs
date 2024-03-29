using ProtoBuf;
using VoxInvasion.Server.Core.Packets.Entrance;
using VoxInvasion.Server.Core.Packets.Entrance.Login;
using VoxInvasion.Server.Core.Packets.Entrance.Registration;
using VoxInvasion.Server.Core.Packets.Entrance.Validation;
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
[ProtoInclude(800, typeof(LoginFromOtherConnectionPacket))]
[ProtoInclude(900, typeof(PongPacket))]
[ProtoInclude(1000, typeof(PingResultPacket))]
[ProtoInclude(1100, typeof(CheckEmailPacket))]
[ProtoInclude(1200, typeof(EmailInvalidPacket))]
[ProtoInclude(1300, typeof(EmailOccupiedPacket))]
[ProtoInclude(1400, typeof(EmailValidPacket))]
[ProtoInclude(1500, typeof(CheckUsernamePacket))]
[ProtoInclude(1600, typeof(UsernameOccupiedPacket))]
[ProtoInclude(1700, typeof(UsernameValidPacket))]
public interface IPacket
{
    PacketId Id { get; }
}