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
    /// <summary>
    /// [[#sec4.20|Audio encryption]]
    /// </summary>
    public static ReadOnlySpan<byte> AENC => "AENC"u8;

    /// <summary>
    /// [#sec4.15 Attached picture]
    /// </summary>
    public static ReadOnlySpan<byte> APIC => "APIC"u8;

    /// <summary>
    /// [#sec4.11 Comments]
    /// </summary>
    public static ReadOnlySpan<byte> COMM => "COMM"u8;

    /// <summary>
    /// [#sec4.25 Commercial frame]
    /// </summary>
    public static ReadOnlySpan<byte> COMR => "COMR"u8;

    /// <summary>
    /// [#sec4.26 Encryption method registration]
    /// </summary>
    public static ReadOnlySpan<byte> ENCR => "ENCR"u8;

    /// <summary>
    /// [#sec4.13 Equalization]
    /// </summary>
    public static ReadOnlySpan<byte> EQUA => "EQUA"u8;

    /// <summary>
    /// [#sec4.6 Event timing codes]
    /// </summary>
    public static ReadOnlySpan<byte> ETCO => "ETCO"u8;

    /// <summary>
    /// [#sec4.16 General encapsulated object]
    /// </summary>
    public static ReadOnlySpan<byte> GEOB => "GEOB"u8;

    /// <summary>
    /// [#sec4.27 Group identification registration]
    /// </summary>
    public static ReadOnlySpan<byte> GRID => "GRID"u8;

    /// <summary>
    /// [#sec4.4 Involved people list]
    /// </summary>
    public static ReadOnlySpan<byte> IPLS => "IPLS"u8;

    /// <summary>
    /// [#sec4.21 Linked information]
    /// </summary>
    public static ReadOnlySpan<byte> LINK => "LINK"u8;

    /// <summary>
    /// [#sec4.5 Music CD identifier]
    /// </summary>
    public static ReadOnlySpan<byte> MCDI => "MCDI"u8;

    /// <summary>
    /// [#sec4.7 MPEG location lookup table]
    /// </summary>
    public static ReadOnlySpan<byte> MLLT => "MLLT"u8;

    /// <summary>
    /// [#sec4.24 Ownership frame]
    /// </summary>
    public static ReadOnlySpan<byte> OWNE => "OWNE"u8;

    /// <summary>
    /// [#sec4.28 Private frame]
    /// </summary>
    public static ReadOnlySpan<byte> PRIV => "PRIV"u8;

    /// <summary>
    /// [#sec4.17 Play counter]
    /// </summary>
    public static ReadOnlySpan<byte> PCNT => "PCNT"u8;

    /// <summary>
    /// [#sec4.18 Popularimeter]
    /// </summary>
    public static ReadOnlySpan<byte> POPM => "POPM"u8;

    /// <summary>
    /// [#sec4.22 Position synchronisation frame]
    /// </summary>
    public static ReadOnlySpan<byte> POSS => "POSS"u8;

    /// <summary>
    /// [#sec4.19 Recommended buffer size]
    /// </summary>
    public static ReadOnlySpan<byte> RBUF => "RBUF"u8;

    /// <summary>
    /// [#sec4.12 Relative volume adjustment]
    /// </summary>
    public static ReadOnlySpan<byte> RVAD => "RVAD"u8;

    /// <summary>
    /// [#sec4.14 Reverb]
    /// </summary>
    public static ReadOnlySpan<byte> RVRB => "RVRB"u8;

    /// <summary>
    /// [#sec4.10 Synchronized lyric/text]
    /// </summary>
    public static ReadOnlySpan<byte> SYLT => "SYLT"u8;

    /// <summary>
    /// [#sec4.8 Synchronized tempo codes]
    /// </summary>
    public static ReadOnlySpan<byte> SYTC => "SYTC"u8;

    /// <summary>
    /// [#TALB Album/Movie/Show title]
    /// </summary>
    public static ReadOnlySpan<byte> TALB => "TALB"u8;

    /// <summary>
    /// [#TBPM BPM (beats per minute)]
    /// </summary>
    public static ReadOnlySpan<byte> TBPM => "TBPM"u8;

    /// <summary>
    /// [#TCOM Composer]
    /// </summary>
    public static ReadOnlySpan<byte> TCOM => "TCOM"u8;

    /// <summary>
    /// [#TCON Content type]
    /// </summary>
    public static ReadOnlySpan<byte> TCON => "TCON"u8;

    /// <summary>
    /// [#TCOP Copyright message]
    /// </summary>
    public static ReadOnlySpan<byte> TCOP => "TCOP"u8;

    /// <summary>
    /// [#TDAT Date]
    /// </summary>
    public static ReadOnlySpan<byte> TDAT => "TDAT"u8;

    /// <summary>
    /// [#TDLY Playlist delay]
    /// </summary>
    public static ReadOnlySpan<byte> TDLY => "TDLY"u8;

    /// <summary>
    /// [#TENC Encoded by]
    /// </summary>
    public static ReadOnlySpan<byte> TENC => "TENC"u8;

    /// <summary>
    /// [#TEXT Lyricist/Text writer]
    /// </summary>
    public static ReadOnlySpan<byte> TEXT => "TEXT"u8;

    /// <summary>
    /// [#TFLT File type]
    /// </summary>
    public static ReadOnlySpan<byte> TFLT => "TFLT"u8;

    /// <summary>
    /// [#TIME Time]
    /// </summary>
    public static ReadOnlySpan<byte> TIME => "TIME"u8;
    
    /// <summary>
    /// [#TIT1 Content group description]
    /// </summary>
    public static ReadOnlySpan<byte> TIT1 => "TIT1"u8;

    /// <summary>
    /// [#TIT2 Title/songname/content description]
    /// </summary>
    public static ReadOnlySpan<byte> TIT2 => "TIT2"u8;

    /// <summary>
    /// [#TIT3 Subtitle/Description refinement]
    /// </summary>
    public static ReadOnlySpan<byte> TIT3 => "TIT3"u8;

    /// <summary>
    /// [#TKEY Initial key]
    /// </summary>
    public static ReadOnlySpan<byte> TKEY => "TKEY"u8;

    /// <summary>
    /// [#TLAN Language(s)]
    /// </summary>
    public static ReadOnlySpan<byte> TLAN => "TLAN"u8;

    /// <summary>
    /// [#TLEN Length]
    /// </summary>
    public static ReadOnlySpan<byte> TLEN => "TLEN"u8;

    /// <summary>
    /// [#TMED Media type]
    /// </summary>
    public static ReadOnlySpan<byte> TMED => "TMED"u8;

    /// <summary>
    /// [#TOAL Original album/movie/show title]
    /// </summary>
    public static ReadOnlySpan<byte> TOAL => "TOAL"u8;

    /// <summary>
    /// [#TOFN Original filename]
    /// </summary>
    public static ReadOnlySpan<byte> TOFN => "TOFN"u8;

    /// <summary>
    /// [#TOLY Original lyricist(s)/text writer(s)]
    /// </summary>
    public static ReadOnlySpan<byte> TOLY => "TOLY"u8;

    /// <summary>
    /// [#TOPE Original artist(s)/performer(s)]
    /// </summary>
    public static ReadOnlySpan<byte> TOPE => "TOPE"u8;

    /// <summary>
    /// [#TORY Original release year]
    /// </summary>
    public static ReadOnlySpan<byte> TORY => "TORY"u8;

    /// <summary>
    /// [#TOWN File owner/licensee]
    /// </summary>
    public static ReadOnlySpan<byte> TOWN => "TOWN"u8;

    /// <summary>
    /// [#TPE1 Lead performer(s)/Soloist(s)]
    /// </summary>
    public static ReadOnlySpan<byte> TPE1 => "TPE1"u8;
    /// <summary>
    /// [#TPE2 Band/orchestra/accompaniment]
    /// </summary>
    public static ReadOnlySpan<byte> TPE2 => "TPE2"u8;
    /// <summary>
    /// [#TPE3 Conductor/performer refinement]
    /// </summary>
    public static ReadOnlySpan<byte> TPE3 => "TPE3"u8;
    /// <summary>
    /// [#TPE4 Interpreted, remixed, or otherwise modified by]
    /// </summary>
    public static ReadOnlySpan<byte> TPE4 => "TPE4"u8;

    /// <summary>
    /// [#TPOS Part of a set]
    /// </summary>
    public static ReadOnlySpan<byte> TPOS => "TPOS"u8;

    /// <summary>
    /// [#TPUB Publisher]
    /// </summary>
    public static ReadOnlySpan<byte> TPUB => "TPUB"u8;

    /// <summary>
    /// [#TRCK Track number/Position in set]
    /// </summary>
    public static ReadOnlySpan<byte> TRCK => "TRCK"u8;

    /// <summary>
    /// [#TRDA Recording dates]
    /// </summary>
    public static ReadOnlySpan<byte> TRDA => "TRDA"u8;

    /// <summary>
    /// [#TRSN Internet radio station name]
    /// </summary>
    public static ReadOnlySpan<byte> TRSN => "TRSN"u8;

    /// <summary>
    /// [#TRSO Internet radio station owner]
    /// </summary>
    public static ReadOnlySpan<byte> TRSO => "TRSO"u8;

    /// <summary>
    /// [#TSIZ Size]
    /// </summary>
    public static ReadOnlySpan<byte> TSIZ => "TSIZ"u8;

    /// <summary>
    /// [#TSRC ISRC (international standard recording code)]
    /// </summary>
    public static ReadOnlySpan<byte> TSRC => "TSRC"u8;

    /// <summary>
    /// [#TSEE Software/Hardware and settings used for encoding]
    /// </summary>
    public static ReadOnlySpan<byte> TSSE => "TSSE"u8;

    /// <summary>
    /// [#TYER Year]
    /// </summary>
    public static ReadOnlySpan<byte> TYER => "TYER"u8;

    /// <summary>
    /// [#TXXX User defined text information frame]
    /// </summary>
    public static ReadOnlySpan<byte> TXXX => "TXXX"u8;

    /// <summary>
    /// [#sec4.1 Unique file identifier]
    /// </summary>
    public static ReadOnlySpan<byte> UFID => "UFID"u8;

    /// <summary>
    /// [#sec4.23 Terms of use]
    /// </summary>
    public static ReadOnlySpan<byte> USER => "USER"u8;

    /// <summary>
    /// [#sec4.9 Unsychronized lyric/text transcription]
    /// </summary>
    public static ReadOnlySpan<byte> USLT => "USLT"u8;

    /// <summary>
    /// [#WCOM Commercial information]
    /// </summary>
    public static ReadOnlySpan<byte> WCOM => "WCOM"u8;

    /// <summary>
    /// [#WCOP Copyright/Legal information]
    /// </summary>
    public static ReadOnlySpan<byte> WCOP => "WCOP"u8;

    /// <summary>
    /// [#WOAF Official audio file webpage]
    /// </summary>
    public static ReadOnlySpan<byte> WOAF => "WOAF"u8;

    /// <summary>
    /// [#WOAR Official artist/performer webpage]
    /// </summary>
    public static ReadOnlySpan<byte> WOAR => "WOAR"u8;

    /// <summary>
    /// [#WOAS Official audio source webpage]
    /// </summary>
    public static ReadOnlySpan<byte> WOAS => "WOAS"u8;

    /// <summary>
    /// [#WORS Official internet radio station homepage]
    /// </summary>
    public static ReadOnlySpan<byte> WORS => "WORS"u8;

    /// <summary>
    /// [#WPAY Payment]
    /// </summary>
    public static ReadOnlySpan<byte> WPAY => "WPAY"u8;

    /// <summary>
    /// [#WPUB Publishers official webpage]
    /// </summary>
    public static ReadOnlySpan<byte> WPUB => "WPUB"u8;

    /// <summary>
    /// [#WXXX User defined URL link frame]
    /// </summary>
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