using ProtoBuf;
using VoxInvasion.Server.Core.Servers;

namespace VoxInvasion.Server.Core.Packets.Entrance;

[ProtoContract]
public class WelcomePacket : IPacket
{
    [ProtoMember(1)] public PacketId Id { get; } = PacketId.Welcome;
    [ProtoMember(2)] public string Message { get; init; } = null!;
}

public class WelcomeHandler : IPacketHandler
{
    public PacketId Id { get; } = PacketId.Welcome;

    public void Execute(IPacket packet, PlayerConnection connection)
    {
        connection.Logger.Information($"Message from client: {((WelcomePacket)packet).Message}");
    }
}