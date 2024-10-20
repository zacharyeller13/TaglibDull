namespace TaglibDull.Frame;

/// <summary>
/// A text information frame defined by <see cref="FrameType"/> <see cref="FrameType.TXXX"/>
/// </summary>
/// <remarks>
/// Doesn't inherit from TextInformationFrame b/c that is I think an overly complex inheritance structure.  We can just copy a few fields.
/// As spec states, we can have many TXXX tags, but only one of each <see cref="Description"/>.  This will be handled by
/// any class that uses this one.
/// </remarks>
public class UserTextInformationFrame : Frame
{
    private readonly byte[]? _unicodeBOM = null;

    private readonly byte[] _description;
    private readonly byte[] _value;

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
    /// Null byte terminated description of the user-defined text frame
    /// &lt;text string according to encoding&gt; $00 (00)
    /// </summary>
    public ReadOnlySpan<byte> Description => _description;

    /// <summary>
    /// Text string according to encoding excluding the <see cref="UnicodeBOM"/>
    /// </summary>
    /// <remarks>
    /// If empty it will be something like <c>0x00 0x00</c>
    /// </remarks>
    public ReadOnlySpan<byte> Value => _value;
    
    public UserTextInformationFrame(ReadOnlySpan<byte> data) : base(data)
    {
    }
}