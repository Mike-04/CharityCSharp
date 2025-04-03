using MessagePack;
using Charity.Network.ObjectProtocol;

public static class MessagePackSerializerHelper
{
    public static byte[] Serialize(IRequest request)
    {
        return MessagePackSerializer.Serialize(request);
    }

    public static IRequest DeserializeRequest(byte[] data)
    {
        return MessagePackSerializer.Deserialize<IRequest>(data);
    }

    public static byte[] Serialize(IResponse response)
    {
        return MessagePackSerializer.Serialize(response);
    }

    public static IResponse DeserializeResponse(byte[] data)
    {
        return MessagePackSerializer.Deserialize<IResponse>(data);
    }
}