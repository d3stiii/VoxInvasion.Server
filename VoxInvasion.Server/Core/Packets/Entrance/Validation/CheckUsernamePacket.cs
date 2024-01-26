using ProtoBuf;
using VoxInvasion.Server.Core.Servers;

namespace VoxInvasion.Server.Core.Packets.Entrance.Validation;

[ProtoContract]
public class CheckUsernamePacket : IPacket
{
    [ProtoMember(1)] public PacketId Id { get; } = PacketId.CheckUsername;
    [ProtoMember(2)] public string Username { get; init; } = null!;
}

public class CheckUsernameHandler : IPacketHandler
{
    public PacketId Id { get; } = PacketId.CheckUsername;

    public void Execute(IPacket packet, PlayerConnection connection)
    {
        var checkUsernamePacket = (CheckUsernamePacket)packet;
        var db = connection.ConnectedServer.Context;
        if (db.Players.Any(player => player.Username == checkUsernamePacket.Username))
            connection.SendAsync(new UsernameOccupiedPacket());
        else
            connection.SendAsync(new UsernameValidPacket());

        //TODO: Check username length and etc
    }
}