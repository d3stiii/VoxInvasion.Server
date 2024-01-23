using Serilog;
using VoxInvasion.Server.Core.Database.Models;
using VoxInvasion.Server.Core.Protocol.Packets;
using VoxInvasion.Server.Core.Protocol.Packets.Authentication.Login;
using VoxInvasion.Server.Core.Servers;
using VoxInvasion.Server.Core.Utilities;

namespace VoxInvasion.Server.Core.Protocol.Handlers.Authentication.Login;

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

        connection.PlayerData = playerData;
        connection.SendAsync(new LoginSuccessPacket());
        Logger.Information($"{playerData.Username} authed successfully");
    }
}