using System.Buffers.Binary;
using System.Text;

namespace TaglibDull.Frame;

// TODO: Consider if a fixed array and unsafe keyword would be applicable?
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

    /// <summary>
    /// Size of a specific <c>Frame</c> less the size of a <c>FrameHeader</c>. (frame size - 10)
    /// </summary>
    public uint Size => _size;

    /// <summary>
    /// Represents the FrameHeader flags. <see href="https://id3.org/id3v2.3.0#Frame_header_flags">ID3.org</see> 
    /// </summary>
    /// <remarks>
    /// Not currently being parsed or validated - just passing straight through as a span of bytes.
    /// </remarks>
    public ReadOnlySpan<byte> Flags => _flags;

    /// <summary>
    /// Size of a frame header
    /// </summary>
    /// <remarks>
    /// Like the <see cref="Header.Size">Tag Header Size</see>, is always 10 for ID3 tags
    /// </remarks>
    public const uint FrameHeaderSize = 10;

    public FrameHeader(ReadOnlySpan<byte> data)
    {
        if (data.Length < FrameHeaderSize)
        {
            throw new FormatException(
                $"Not enough bytes to determine frame header. Needed {FrameHeaderSize}, got {data.Length}");
        }

        if (!FrameType.IsValid(data[..4]))
        {
            throw new FormatException(
                $"Read non-standard or unsupported frame type {Encoding.UTF8.GetString(data[..4])}");
        }

        _frameId = data[..4].ToArray();
        _size = BinaryPrimitives.ReadUInt32BigEndian(data[4..8]);
        _flags = data[8..10].ToArray();
    }
}
