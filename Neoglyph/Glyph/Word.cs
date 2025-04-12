namespace Neoglyph.Glyph;

public readonly struct Word(Glyph[] glyphs)
{
	public Glyph[] Glyphs { get; private init; } = glyphs;
}