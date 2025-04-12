namespace Neoglyph.Glyph;

public readonly struct Glyph
{
	public Glyph(string symbols)
	{
		List<Symbol> list = [];
		foreach (var symbol in symbols) {
			list.Add(new Symbol(symbol));
		}
		
		Symbols = list.ToArray();
	}
	
	/// <summary>
	/// Parts of a glyph. Goes in order [Left, Top, Right, Bottom]
	/// </summary>
	public Symbol[] Symbols { get; private init; }
}