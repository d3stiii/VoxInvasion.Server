using System.Net.Mail;
using ProtoBuf;
using VoxInvasion.Server.Core.Servers;

namespace VoxInvasion.Server.Core.Packets.Entrance.Validation;

[ProtoContract]
public class CheckEmailPacket : IPacket
{
    [ProtoMember(1)] public PacketId Id { get; } = PacketId.CheckEmail;
    [ProtoMember(2)] public string Email { get; init; } = null!;
}

public class CheckEmailHandler : IPacketHandler
{
    public PacketId Id { get; } = PacketId.CheckEmail;

    public void Execute(IPacket packet, PlayerConnection connection)
    {
        var checkEmailPacket = (CheckEmailPacket)packet;
        try
        {
            MailAddress email = new(checkEmailPacket.Email);

            var db = connection.ConnectedServer.Context;
            if (db.Players.Any(player => player.Email == email.Address))
            {
                connection.SendAsync(new EmailOccupiedPacket());
            }
            else
            {
                connection.SendAsync(new EmailValidPacket());
            }
        }
        catch
        {
            connection.SendAsync(new EmailInvalidPacket());
        }
    }
}