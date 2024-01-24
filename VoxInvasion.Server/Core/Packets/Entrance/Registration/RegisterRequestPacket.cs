using ProtoBuf;
using Serilog;
using VoxInvasion.Server.Core.Database.Models;
using VoxInvasion.Server.Core.Packets.Entrance.Login;
using VoxInvasion.Server.Core.Servers;
using VoxInvasion.Server.Core.Utilities;

namespace VoxInvasion.Server.Core.Packets.Entrance.Registration;

[ProtoContract]
public class RegisterRequestPacket : IPacket
{
    [ProtoMember(1)] public PacketId Id { get; } = PacketId.RegisterRequest;
    [ProtoMember(2)] public string Username { get; init; } = null!;
    [ProtoMember(3)] public string Email { get; init; } = null!;
    [ProtoMember(4)] public string Password { get; init; } = null!;
}

public class RegisterRequestHandler : IPacketHandler
{
    private static readonly ILogger Logger = Log.Logger.ForType(typeof(RegisterRequestHandler));
    public PacketId Id { get; } = PacketId.RegisterRequest;

    public void Execute(IPacket packet, PlayerConnection connection)
    {
        var request = (RegisterRequestPacket)packet;

        var dbContext = connection.ConnectedServer.Context;
        if (dbContext.Players.Any(player => player.Username == request.Username))
        {
            connection.SendAsync(new RegistrationFailedPacket());
            return;
        }

        connection.PlayerData = new Player
        {
            Id = Guid.NewGuid(),
            DateCreated = DateTimeOffset.UtcNow,
            DateUpdated = DateTime.UtcNow,
            Email = request.Email,
            Username = request.Username,
            Password = request.Password
        };
        connection.ConnectedServer.Context.Players.Add(connection.PlayerData);
        connection.ConnectedServer.Context.SaveChanges();

        connection.SendAsync(new LoginSuccessPacket());
        connection.Logger = connection.Logger.WithPlayer(connection);
        connection.Logger.Information($"Player registered successfully");
    }
}