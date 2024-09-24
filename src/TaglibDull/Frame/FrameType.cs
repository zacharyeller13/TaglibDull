namespace TaglibDull.Frame;

/// <summary>
/// Represents all declared frame types in ID3v2.3
/// <see href="https://id3.org/id3v2.3.0#Declared_ID3v2_frames">ID3.org</see>
/// </summary>
/// <remarks>
/// We do it this way instead of an Enum, because it's easier for <c>Span</c> and <c>byte[]</c> comparisons whereas
/// enum members can only be easily converted to or cast as normal strings.
/// Then caching all the properties as a <c>HashSet&lt;byte[]&gt;</c> <see cref="Types"/> so we can do an easy <see cref="IsValid(ReadOnlySpan&lt;byte&gt;)"/> check
/// </remarks>
public static class FrameType
{
    // ReSharper disable InconsistentNaming
    public static ReadOnlySpan<byte> AENC => "AENC"u8;
    public static ReadOnlySpan<byte> APIC => "APIC"u8;
    public static ReadOnlySpan<byte> COMM => "COMM"u8;
    public static ReadOnlySpan<byte> COMR => "COMR"u8;
    public static ReadOnlySpan<byte> ENCR => "ENCR"u8;
    public static ReadOnlySpan<byte> EQUA => "EQUA"u8;
    public static ReadOnlySpan<byte> ETCO => "ETCO"u8;
    public static ReadOnlySpan<byte> GEOB => "GEOB"u8;
    public static ReadOnlySpan<byte> GRID => "GRID"u8;
    public static ReadOnlySpan<byte> IPLS => "IPLS"u8;
    public static ReadOnlySpan<byte> LINK => "LINK"u8;
    public static ReadOnlySpan<byte> MCDI => "MCDI"u8;
    public static ReadOnlySpan<byte> MLLT => "MLLT"u8;
    public static ReadOnlySpan<byte> OWNE => "OWNE"u8;
    public static ReadOnlySpan<byte> PRIV => "PRIV"u8;
    public static ReadOnlySpan<byte> PCNT => "PCNT"u8;
    public static ReadOnlySpan<byte> POPM => "POPM"u8;
    public static ReadOnlySpan<byte> POSS => "POSS"u8;
    public static ReadOnlySpan<byte> RBUF => "RBUF"u8;
    public static ReadOnlySpan<byte> RVAD => "RVAD"u8;
    public static ReadOnlySpan<byte> RVRB => "RVRB"u8;
    public static ReadOnlySpan<byte> SYLT => "SYLT"u8;
    public static ReadOnlySpan<byte> SYTC => "SYTC"u8;
    public static ReadOnlySpan<byte> TALB => "TALB"u8;
    public static ReadOnlySpan<byte> TBPM => "TBPM"u8;
    public static ReadOnlySpan<byte> TCOM => "TCOM"u8;
    public static ReadOnlySpan<byte> TCON => "TCON"u8;
    public static ReadOnlySpan<byte> TCOP => "TCOP"u8;
    public static ReadOnlySpan<byte> TDAT => "TDAT"u8;
    public static ReadOnlySpan<byte> TDLY => "TDLY"u8;
    public static ReadOnlySpan<byte> TENC => "TENC"u8;
    public static ReadOnlySpan<byte> TEXT => "TEXT"u8;
    public static ReadOnlySpan<byte> TFLT => "TFLT"u8;
    public static ReadOnlySpan<byte> TIME => "TIME"u8;
    public static ReadOnlySpan<byte> TIT1 => "TIT1"u8;
    public static ReadOnlySpan<byte> TIT2 => "TIT2"u8;
    public static ReadOnlySpan<byte> TIT3 => "TIT3"u8;
    public static ReadOnlySpan<byte> TKEY => "TKEY"u8;
    public static ReadOnlySpan<byte> TLAN => "TLAN"u8;
    public static ReadOnlySpan<byte> TLEN => "TLEN"u8;
    public static ReadOnlySpan<byte> TMED => "TMED"u8;
    public static ReadOnlySpan<byte> TOAL => "TOAL"u8;
    public static ReadOnlySpan<byte> TOFN => "TOFN"u8;
    public static ReadOnlySpan<byte> TOLY => "TOLY"u8;
    public static ReadOnlySpan<byte> TOPE => "TOPE"u8;
    public static ReadOnlySpan<byte> TORY => "TORY"u8;
    public static ReadOnlySpan<byte> TOWN => "TOWN"u8;
    public static ReadOnlySpan<byte> TPE1 => "TPE1"u8;
    public static ReadOnlySpan<byte> TPE2 => "TPE2"u8;
    public static ReadOnlySpan<byte> TPE3 => "TPE3"u8;
    public static ReadOnlySpan<byte> TPE4 => "TPE4"u8;
    public static ReadOnlySpan<byte> TPOS => "TPOS"u8;
    public static ReadOnlySpan<byte> TPUB => "TPUB"u8;
    public static ReadOnlySpan<byte> TRCK => "TRCK"u8;
    public static ReadOnlySpan<byte> TRDA => "TRDA"u8;
    public static ReadOnlySpan<byte> TRSN => "TRSN"u8;
    public static ReadOnlySpan<byte> TRSO => "TRSO"u8;
    public static ReadOnlySpan<byte> TSIZ => "TSIZ"u8;
    public static ReadOnlySpan<byte> TSRC => "TSRC"u8;
    public static ReadOnlySpan<byte> TSSE => "TSSE"u8;
    public static ReadOnlySpan<byte> TYER => "TYER"u8;
    public static ReadOnlySpan<byte> TXXX => "TXXX"u8;
    public static ReadOnlySpan<byte> UFID => "UFID"u8;
    public static ReadOnlySpan<byte> USER => "USER"u8;
    public static ReadOnlySpan<byte> USLT => "USLT"u8;
    public static ReadOnlySpan<byte> WCOM => "WCOM"u8;
    public static ReadOnlySpan<byte> WCOP => "WCOP"u8;
    public static ReadOnlySpan<byte> WOAF => "WOAF"u8;
    public static ReadOnlySpan<byte> WOAR => "WOAR"u8;
    public static ReadOnlySpan<byte> WOAS => "WOAS"u8;
    public static ReadOnlySpan<byte> WORS => "WORS"u8;
    public static ReadOnlySpan<byte> WPAY => "WPAY"u8;
    public static ReadOnlySpan<byte> WPUB => "WPUB"u8;
    public static ReadOnlySpan<byte> WXXX => "WXXX"u8;

    public static HashSet<byte[]> Types { get; } = SetTypes();

    private static HashSet<byte[]> SetTypes()
    {
        var properties = typeof(FrameType).GetProperties();
        return properties.Select(t => t.GetValue(null) as byte[]).ToHashSet()!;
    }

    public static bool IsValid(string type) => typeof(FrameType).GetProperty(type) is not null;

    public static bool IsValid(ReadOnlySpan<byte> type) => Types.Contains(type.ToArray());
}