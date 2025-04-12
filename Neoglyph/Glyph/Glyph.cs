namespace Neoglyph.Glyph;

public static class Glyph
{
	public static Symbol SymbolFromChar(char ch)
	{
		return ch switch {
			'|' => Symbol.Vertical,
			'-' => Symbol.Horizontal,
			'+' => Symbol.Cross,
			_ => throw new FormatException("Unknown char")
		};
	}
	
	public enum Symbol
    {
    	Vertical,
    	Horizontal,
    	Cross
    }
}