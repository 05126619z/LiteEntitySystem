using System.Runtime.InteropServices;

namespace LiteEntitySystem.Internal
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct BaselineDataHeader
    {
        public byte UserHeader;
        public byte PacketType;
        public byte PlayerId;
        public byte SendRate;
        public ushort Tick;
        public byte Tickrate;
        public int OriginalLength;
        public int EventsSize;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct LastPartData
    {
        public ushort Mtu;
        public ushort LastProcessedTick;
        public ushort LastReceivedTick;
        public byte BufferedInputsCount;
        public int EventsSize;
    }
    
    [StructLayout(LayoutKind.Sequential)]
    internal struct DiffPartHeader
    {
        public byte UserHeader;
        public byte PacketType;
        public byte Part;
        public ushort Tick;
    }
    
    internal struct InputPacketHeader
    {
        public ushort StateA;
        public ushort StateB;
        public float LerpMsec;
        public static readonly unsafe int Size = sizeof(InputPacketHeader);
    }

    /// <summary>
    /// One client to server rpc call in a reliable <see cref="InternalPackets.ClientRPC"/> packet (header byte, packet
    /// type, then calls one after another), followed by ByteCount bytes of argument.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct ServerCallHeader
    {
        /// <summary>Client tick the call belongs to: the server executes it after that tick's input.</summary>
        public ushort Tick;
        public ushort EntityId;
        public byte EntityVersion;
        public ushort RpcId;
        public ushort ByteCount;
        public static readonly unsafe int Size = sizeof(ServerCallHeader);
    }

    internal static class InternalPackets
    {
        public const byte DiffSync = 1;
        public const byte ClientInput = 2;
        public const byte BaselineSync = 3;
        public const byte DiffSyncLast = 4;
        public const byte ClientRequest = 5;
        public const byte ClientRPC = 6;
    }
}