namespace Neoglyph.Glyph;

public readonly struct Glyph
{
	public Glyph(string symbols)
	{
		foreach (var symbol in symbols) {
			Part[] parts = symbol switch {
				'|' => [Part.None, Part.Line, Part.None, Part.Line],
				'-' => [Part.Line, Part.None, Part.Line, Part.None],
				'+' => [Part.Line, Part.Line, Part.Line, Part.Line],
				_ => throw new FormatException("Unknown symbol")
			};
			
			Symbols.Add(parts);
		}
	}

	/// <summary>
	/// Parts of a glyph. Goes in order [Left, Top, Right, Bottom]
	/// </summary>
	public List<Part[]> Symbols { get; private init; } = [];
	
	public enum Part
	{
		Line,
		None
	}
}