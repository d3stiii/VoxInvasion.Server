using VoxInvasion.Server.Core.Servers;

namespace VoxInvasion.Server.Core.Packets;

public interface IPacketHandler
{
    PacketId Id { get; }
    void Execute(IPacket packet, PlayerConnection connection);
}