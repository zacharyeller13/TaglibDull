using System.Buffers.Binary;

namespace TaglibDull;

/// <summary>
/// Represents an individual ID3v2 Frame Header.
/// <see href="https://id3.org/id3v2.3.0#ID3v2_frame_overview">ID3.org</see>
/// </summary>
public struct FrameHeader
{
    private readonly byte[] _frameId;
    private readonly uint _size;
    private readonly byte[] _flags;
    
    public ReadOnlySpan<byte> FrameId => _frameId;
    public uint Size => _size;
    public ReadOnlySpan<byte> Flags => _flags;

    public FrameHeader(ReadOnlySpan<byte> data)
    {
        _frameId = data[..4].ToArray();
        _size = BinaryPrimitives.ReadUInt32BigEndian(data[4..8]);
        _flags = data[8..10].ToArray();
    }
}