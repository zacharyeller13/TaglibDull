namespace TaglibDull.Frame;

/// <summary>
/// Represents one of the declared ID3v2 frames <see href="https://id3.org/id3v2.3.0#Declared_ID3v2_frames">ID3.org</see>
/// </summary>
/// <remarks>This is to be just a base class for the more detailed frame types, such as Text information frames</remarks>
public abstract class Frame
{
    public FrameHeader Header { get; }

    protected Frame(ReadOnlySpan<byte> data)
    {
        Header = new FrameHeader(data);
    }
}
