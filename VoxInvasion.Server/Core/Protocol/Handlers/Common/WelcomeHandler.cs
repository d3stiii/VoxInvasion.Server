using VoxInvasion.Server.Core.Protocol.Packets;
using VoxInvasion.Server.Core.Protocol.Packets.Common;
using VoxInvasion.Server.Core.Servers;

namespace VoxInvasion.Server.Core.Protocol.Handlers.Common;

public class WelcomeHandler : IPacketHandler
{
    public PacketId Id { get; } = PacketId.Welcome;
    public void Execute(IPacket packet, PlayerConnection connection)
    {
        Console.WriteLine(((WelcomePacket) packet).Message);
    }
}