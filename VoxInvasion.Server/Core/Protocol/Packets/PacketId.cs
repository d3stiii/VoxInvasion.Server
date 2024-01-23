namespace VoxInvasion.Server.Core.Protocol.Packets;

public enum PacketId
{
    Welcome,
    Ping,
    LoginRequest,
    RegisterRequest,
    RegistrationFailed,
    LoginSuccess,
    LoginFailed
}