using System.Diagnostics;
using System.Text;

namespace TaglibDull.Frame;

/// <summary>
/// A text information frame defined by any <see cref="FrameType"/> starting with 'T' except "TXXX"
/// </summary>
/// <remarks>
/// The text information frames are the most important frames, containing information like artist, album and more.
/// There may only be one text information frame of its kind in an tag.
/// If the textstring is followed by a termination ($00 (00)) all the following information should be ignored and not be displayed. 
/// </remarks>
public class TextInformationFrame : Frame
{
    private readonly byte[]? _unicodeBOM;

    private readonly byte[] _information;

    /// <summary>
    /// Represents text encoding for this frame.
    /// </summary>
    /// <list type="table">
    /// <item><term>0x00</term><description>ISO-8859-1</description></item>
    /// <item><term>0x01</term><description>Unicode</description></item>
    /// </list>
    /// <remarks>
    /// All numeric strings and URLs are always encoded as ISO-8859-1. Terminated strings are terminated with 0x00 if encoded with ISO-8859-1 and 0x00 0x00 if encoded as unicode.
    /// If nothing else is said newline character is forbidden. In ISO-8859-1 a new line is represented, when allowed, with 0x0A only. 
    /// Frames that allow different types of text encoding have a text encoding description byte directly after the frame size. 
    /// If ISO-8859-1 is used this byte should be 00, if Unicode is used it should be 0x01.
    /// Any empty Unicode strings which are NULL-terminated may have the Unicode BOM followed by a Unicode NULL (0xFF 0xFE 0x00 0x00, or 0xFE 0xFF 0x00 0x00). 
    /// </remarks>
    public byte TextEncoding { get; }

    /// <summary>
    /// The BOM when this text frame uses Unicode <see cref="TextEncoding"/> to signify byte order
    /// </summary>
    /// <list type="table">
    /// <item><term>0xFF 0xFE</term><description>BigEndian</description></item>
    /// <item><term>0xFE 0xFF</term><description>LittleEndian</description></item>
    /// </list>
    public ReadOnlySpan<byte> UnicodeBOM => _unicodeBOM ?? ReadOnlySpan<byte>.Empty;

    /// <summary>
    /// Text string according to encoding excluding the <see cref="UnicodeBOM"/>
    /// </summary>
    /// <remarks>
    /// If empty it will be something like <c>0x00 0x00</c>
    /// </remarks>
    public ReadOnlySpan<byte> Information => _information;

    public TextInformationFrame(ReadOnlySpan<byte> data) : base(data)
    {
        // TextEncoding should always come right after the last FrameHeader, so 0-based index should always be
        // at the index immediately after FrameHeader ends
        int currentByteIdx = (int)FrameHeader.FrameHeaderSize;

        if (!FrameType.IsValidTextFrame(Header.FrameId))
        {
            throw new FormatException(
                $"FrameType {Encoding.UTF8.GetString(Header.FrameId)} is not a valid Text Information Frame");
        }

        TextEncoding = data[currentByteIdx++];
        if (TextEncoding > 0x01)
        {
            // TODO: Just fall back to ISO instead
            throw new FormatException("Text encoding must be either 0x00 (ISO-8859-1) or 0x01 (Unicode)");
        }

        Debug.Assert(TextEncoding is 0x00 or 0x01, "Text encoding isn't 0x00 or 0x01");

        // TODO: Determine end of text string based on encoding
        // ISO == 0x00
        // Unicode == 0x00 0x00
        byte[] nullTerminator = [0x00];
        if (TextEncoding == 0x01)
        {
            // Must be one of 0xFF 0xFE || 0xFE 0xFF
            _unicodeBOM = data.Slice(currentByteIdx, 2).ToArray();
            Debug.Assert(UnicodeBOM.SequenceEqual<byte>([0xFF, 0xFE]) || UnicodeBOM.SequenceEqual<byte>([0xFE, 0xFF]),
                "Unicode BOM isn't valid");

            // Move past the 2 bytes of Unicode BOM
            currentByteIdx += 2;
            // Change what the null terminator is
            nullTerminator = [0x00, 0x00];
        }

        // Get all data based on FrameHeader listed size; excluding the UnicodeBOM (if any) and TextEncoding bytes
        _information = data.Slice(currentByteIdx, (int)Header.Size - (UnicodeBOM.Length + sizeof(byte))).ToArray();
        Debug.Assert(_information[^2..] != nullTerminator, "There's no null terminator for some reason??");


        // All information should match the size FrameHeader tells us it is
        uint dataSize = (uint)(_information.Length + sizeof(byte) + UnicodeBOM.Length);
        Debug.Assert(dataSize == Header.Size, "Actual frame size doesn't match FrameHeader defined size.");
    }
}