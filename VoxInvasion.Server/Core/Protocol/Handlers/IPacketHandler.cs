using VoxInvasion.Server.Core.Protocol.Packets;
using VoxInvasion.Server.Core.Servers;

namespace VoxInvasion.Server.Core.Protocol.Handlers;

public interface IPacketHandler
{
    PacketId Id { get; }
    void Execute(IPacket packet, PlayerConnection connection);
}