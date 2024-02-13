using ProtoBuf;
using Serilog;
using VoxInvasion.Server.Core.Database.Models;
using VoxInvasion.Server.Core.Servers;
using VoxInvasion.Server.Core.Utilities;

namespace VoxInvasion.Server.Core.Packets.Entrance.Login;

[ProtoContract]
public class LoginRequestPacket : IPacket
{
    [ProtoMember(1)] public PacketId Id { get; } = PacketId.LoginRequest;
    [ProtoMember(2)] public string Username { get; init; } = null!;
    [ProtoMember(3)] public string Password { get; init; } = null!;
}

public class LoginRequestHandler : IPacketHandler
{
    private static readonly ILogger Logger = Log.Logger.ForType(typeof(LoginRequestHandler));
    public PacketId Id { get; } = PacketId.LoginRequest;

    public void Execute(IPacket packet, PlayerConnection connection)
    {
        if (connection.LoggedIn) return;
        var request = (LoginRequestPacket)packet;

        var dbContext = connection.ConnectedServer.Context;
        Player? playerData = dbContext.Players.SingleOrDefault(player =>
            player.Username == request.Username && player.Password == request.Password);
        if (playerData == null)
        {
            connection.SendAsync(new LoginFailedPacket());
            return;
        }

        var playerFromOtherConnection = connection.ConnectedServer.Connections.SingleOrDefault(otherConnection =>
            otherConnection.LoggedIn && otherConnection.PlayerData.Username == request.Username);
        if (playerFromOtherConnection != null)
        {
            playerFromOtherConnection.Logger.Information("Logged in to this account from other connection. Disconnecting...");
            playerFromOtherConnection.SendAsync(new LoginFromOtherConnectionPacket());
            playerFromOtherConnection.Disconnect();
        }

        connection.PlayerData = playerData;
        connection.SendAsync(new LoginSuccessPacket());
        connection.Logger = connection.Logger.WithPlayer(connection);
        connection.Logger.Information($"Player logged in");
    }
}