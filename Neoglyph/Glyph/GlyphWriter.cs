using Neoglyph.Dictionary;
using Neoglyph.Graphics;

namespace Neoglyph.Glyph;

public class GlyphWriter
{
	private readonly Canvas _canvas;
	private readonly Dictionary<char, List<Glyph.Symbol>> _glyphs = [];

	private readonly Cell _symbolOffset = new Cell(1, 0);
	private readonly Cell _charOffset = new Cell(0.5, 0.5);
	private readonly Cell _wordOffset = new Cell(0, 2);
	
	public GlyphWriter(GlyphDictionary dictionary, Canvas canvas)
	{
		_canvas = canvas;
		foreach (var pair in dictionary) {
			List<Glyph.Symbol> symbols = [];
			symbols.AddRange(pair.Value.Select(Glyph.SymbolFromChar));

			_glyphs[pair.Key[0]] = symbols;
		}
	}

	public void DrawCharacter(char c, Cell start)
	{
		var symbols = _glyphs[c];
		var next = start;
		foreach (var symbol in symbols) {
			_canvas.DrawSymbol(symbol, next);
			next += _symbolOffset;
		}
	}

	public void DrawWord(string word, Cell start)
	{
		var next = start;
		foreach (var ch in word) {
			DrawCharacter(ch, next);
			next += _charOffset;
		}
	}

	public void DrawText(string text, Cell start)
	{
		var words = text.Split(' ');
		var next = start;

		foreach (var word in words) {
			DrawWord(word, next);
			next = new Cell(next.x + _wordOffset.x,
							next.y + _wordOffset.y + word.Length / 2.0);
		}
	}
}