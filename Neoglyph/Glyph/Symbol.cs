namespace Neoglyph.Glyph;

public readonly struct Symbol
{
	public Symbol(char symbol)
	{
		Parts = symbol switch {
			'|' => [Part.None, Part.Line, Part.None, Part.Line],
			'-' => [Part.Line, Part.None, Part.Line, Part.None],
			'+' => [Part.Line, Part.Line, Part.Line, Part.Line],
			_ => throw new FormatException("Unknown symbol")
		};
	}
	
	public Part[] Parts { get; private init; }
	
	public enum Part
	{
		Line,
		Arc,
		None
	}
}