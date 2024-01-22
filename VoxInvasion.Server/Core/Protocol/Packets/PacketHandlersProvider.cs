using System.Reflection;
using Serilog;
using VoxInvasion.Server.Core.Protocol.Handlers;
using VoxInvasion.Server.Core.Utilities;

namespace VoxInvasion.Server.Core.Protocol.Packets;

public class PacketHandlersProvider
{
    private static readonly ILogger Logger = Log.Logger.ForType(typeof(PacketHandlersProvider));
    private Dictionary<PacketId, IPacketHandler> _packetHandlers = null!;

    public void Initialize()
    {
        var packetHandlerType = typeof(IPacketHandler);
        _packetHandlers = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => packetHandlerType.IsAssignableFrom(type) && packetHandlerType != type)
            .Select(type => (IPacketHandler)Activator.CreateInstance(type)!)
            .ToDictionary(handler => handler.Id, handler => handler);
        Logger.Information("Packet handlers initialized");
    }

    public IPacketHandler? GetHandler(PacketId packetId) =>
        _packetHandlers.TryGetValue(packetId, out var packetHandler) ? packetHandler : null;

    public void Clear()
    {
        _packetHandlers.Clear();
        Logger.Verbose("Packet handlers cleared");
    }
}