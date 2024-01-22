using ProtoBuf;
using Serilog;
using VoxInvasion.Server.Core.Utilities;

namespace VoxInvasion.Server.Core.Protocol.Packets;

public static class PacketExtensions
{
    private static readonly ILogger Logger = Log.Logger.ForType(typeof(PacketExtensions));

    public static byte[] Serialize(this IPacket packet)
    {
        try
        {
            using var stream = new MemoryStream();
            Serializer.Serialize(stream, packet);
            return stream.ToArray();
        }
        catch (Exception e)
        {
            Logger.Error("Protobuf serialization error: {Exception}", e);
            throw;
        }
    }

    public static IPacket Deserialize(this byte[] data, long offset, long size)
    {
        try
        {
            using var stream = new MemoryStream(data, (int)offset, (int)size);
            return Serializer.Deserialize<IPacket>(stream);
        }
        catch (Exception e)
        {
            Logger.Error("Protobuf deserialization error: {Exception}", e);
            throw;
        }
    }
}