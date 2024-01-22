using ProtoBuf;

namespace VoxInvasion.Server.Core.Protocol.Packets.Common;

[ProtoContract]
public class WelcomePacket : IPacket
{
    [ProtoMember(0)] public PacketId Id { get; } = PacketId.Welcome;
    [ProtoMember(1)] public string Message { get; init; } = null!;
}