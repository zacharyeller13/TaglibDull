using System.Text;

namespace TaglibDull.Frame;

public class UniqueFileIdentifier : Frame
{
    private readonly byte[] _ownerIdentifier;
    private readonly byte[] _identifier;

    /// <summary>
    /// The organization responsible for this specific implementation.
    /// An email or link to an email in the format <c>&lt;text string&gt; 0x00</c>
    /// </summary>
    /// <remarks>
    /// Right now is just an array of bytes that should have a null byte as the last byte.
    /// Maybe move to a <see cref="string" /> at some point.
    /// </remarks>
    public ReadOnlySpan<byte> OwnerIdentifier => _ownerIdentifier;

    /// <summary>
    /// The unique identifier itself.
    /// Up to 64 bytes of binary data
    /// </summary>
    public ReadOnlySpan<byte> Identifier => _identifier;

    public UniqueFileIdentifierFrame(ReadOnlySpan<byte> data) : base(data[..10])
    {
        if (Header.FrameId != FrameType.UFID)
        {
            throw new FormatException($"FrameType {Encoding.UTF8.GetString(Header.FrameId)} is not UFID");
        }

        if (data[10] == 0x00)
        {
            throw new FormatException(
                "UFID Owner Identifier must be non-empty (more than just a null byte (0x00) termination)");
        }

        int nullTerminator = data.IndexOf((byte)0x00);
        _ownerIdentifier = data[10..(nullTerminator + 1)].ToArray();

        _identifier = data.Slice(nullTerminator + 1, (int)Header.Size).ToArray();
    }
}