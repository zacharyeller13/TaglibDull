using System.Diagnostics;
using System.Text;

namespace TaglibDull.Frame;

/// <summary>
/// A frame defined by any <see cref="FrameType"/> starting with 'W' except "WXXX", meant to hold dynamic data like webpages
/// </summary> 
/// <remarks>
/// There may only be one URL link frame of its kind in a tag, except when stated otherwise in the frame description.
/// If the textstring is followed by a termination ($00 (00)) all the following information should be ignored and not be displayed. 
/// All URL link frame identifiers begins with "W". Only URL link frame identifiers begins with "W".
/// </remarks>
public class UserUrlLink : Frame
{
    private readonly byte[]? _unicodeBOM = null;

    private readonly byte[] _description;
    private readonly byte[] _url;

    /// <summary>
    /// Represents text encoding for this frame.
    /// </summary>
    /// <list type="table">
    /// <item><term>0x00</term><description>ISO-8859-1</description></item>
    /// <item><term>0x01</term><description>Unicode</description></item>
    /// </list>
    /// <remarks>
    /// All numeric strings and URLs are always encoded as ISO-8859-1. Terminated strings are terminated with 0x00 if encoded with ISO-8859-1 and 0x00 0x00 if encoded as unicode.
    /// If nothing else is said newline character is forbidden. In ISO-8859-1 a new line is represented, when allowed, with 0x0A only. Frames that allow different types of text encoding have a text encoding description byte directly after the frame size. If ISO-8859-1 is used this byte should be 00, if Unicode is used it should be 0x01. Any empty Unicode strings which are NULL-terminated may have the Unicode BOM followed by a Unicode NULL (0xFF 0xFE 0x00 0x00, or 0xFE 0xFF 0x00 0x00). 
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
    /// Null byte terminated description of the user-defined url frame.
    /// There may be more than one "WXXX" frame in each tag, but only one with the same description.
    /// &lt;text string according to encoding&gt; $00 (00)
    /// </summary>
    public ReadOnlySpan<byte> Description => _description;

    /// <summary>
    /// Text string, always encoded as ISO-8859-1
    /// </summary>
    /// <remarks>
    /// If empty it will be something like <c>0x00</c>
    /// </remarks>
    public ReadOnlySpan<byte> Url => _url;

    public UserUrlLinkFrame(ReadOnlySpan<byte> data) : base(data)
    {
        // Check this is a valid User URL frame
        if (!Header.FrameId.SequenceEqual(FrameType.WXXX))
        {
            throw new FormatException(
                $"FrameType {Encoding.UTF8.GetString(Header.FrameId)} is not a valid URL Link Frame");
        }

        int currentByteIdx = (int)FrameHeader.FrameHeaderSize;
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
        int nullBomAdder = 0;
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
            // We need to check the BOM in order to grab the correct end position of the null terminator
            if (UnicodeBOM.SequenceEqual<byte>([0xFF, 0xFE]))
            {
                // In this case it's 1 because we end up with the last UTF-16 char as 0x## 0x00 + 0x00 0x00 (null term)
                // so 3 consecutive null bytes throws off the indexOf(nullTerminator) check
                nullBomAdder = 1;
            }
        }

        ReadOnlySpan<byte> descAndUrl =
            data.Slice(currentByteIdx, (int)Header.Size - (UnicodeBOM.Length + sizeof(byte)));

        // currentByteIdx should be the start of the description
        // and the first nullTerminator we find should be the end of the description
        _description = descAndUrl[..(descAndUrl.IndexOf(nullTerminator) + nullTerminator.Length + nullBomAdder)].ToArray();

        // value is from end of description to end of this tag
        _url = descAndUrl[_description.Length..].ToArray();

        Debug.Assert(_description[^2..].SequenceEqual(nullTerminator),
            "There's no description null terminator for some reason??");

        // This assert should be incorrect for Url frames since the URL is supposed to be ISO-8859-1 always
        // Debug.Assert(_url[^2..].SequenceEqual(nullTerminator), "There's no value null terminator for some reason??");
    }
}
