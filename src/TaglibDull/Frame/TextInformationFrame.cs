namespace TaglibDull.Frame;

/// <summary>
/// A text information frame defined by any <see cref="FrameType"/> starting with 'T' except "TXXX"
/// </summary>
/// <remarks>
/// The text information frames are the most important frames, containing information like artist, album and more. There may only be one text information frame of its kind in an tag. If the textstring is followed by a termination ($00 (00)) all the following information should be ignored and not be displayed. 
/// </remarks>
public class TextInformationFrame : Frame
{
    private readonly byte[]? _unicodeBOM = null;

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
    /// Text string according to encoding excluding the <see cref="UnicodeBOM"/>
    /// </summary>
    /// <remarks>
    /// If empty it will be something like <c>0x00 0x00</c>
    /// </remarks>
    public ReadOnlySpan<byte> Information => _information;
}
