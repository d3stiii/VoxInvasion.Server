using ProtoBuf;
using VoxInvasion.Server.Core.Protocol.Packets.Common;

namespace VoxInvasion.Server.Core.Protocol.Packets;

[ProtoContract]
[ProtoInclude(100, typeof(WelcomePacket))]
public interface IPacket
{
    PacketId Id { get; }
}