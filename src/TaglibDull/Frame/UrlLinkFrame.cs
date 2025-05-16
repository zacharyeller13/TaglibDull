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
public class UrlLinkFrame : Frame
{
    private readonly byte[] _url;

    public ReadOnlySpan<byte> Url => _url;

    public UrlLinkFrame(ReadOnlySpan<byte> data) : base(data)
    {
        // Check this is a valid URL frame but not the user url frame
        if (!Header.FrameId.StartsWith([(byte)'W']) || Header.FrameId.SequenceEqual(FrameType.WXXX))
        {
            throw new FormatException(
                $"FrameType {Encoding.UTF8.GetString(Header.FrameId)} is not a valid URL Link Frame");
        }

        int currentByteIdx = (int)FrameHeader.FrameHeaderSize;
        _url = data.Slice(currentByteIdx, (int)Header.Size).ToArray();
    }
}
