using Neoglyph.Graphics;

namespace Neoglyph.Glyph;

public class GlyphRenderer(Canvas canvas)
{
	private static readonly Cell _symbolOffset = new Cell(1, 0);
	private static readonly Cell _glyphOffset  = new Cell(0.5, 0.5);
	private static readonly Cell _wordOffset   = new Cell(0, 2);

	public void RenderText(List<Word> words, Cell start)
	{
		var next = start;
		foreach (var word in words) {
			RenderWord(word, next);
			next = new Cell(next.x + _wordOffset.x,
							next.y + _wordOffset.y + word.Glyphs.Length / 2.0);
		}
	}

	public void RenderWord(Word word, Cell start)
	{
		var next = start;
		foreach (var glyph in word.Glyphs) {
			RenderGlyph(glyph, next);
			
			next += _glyphOffset;
		}
	}

	public void RenderGlyph(Glyph glyph, Cell start)
	{
		var next = start;
		foreach (var symbol in glyph.Symbols) {
			canvas.DrawSymbol(symbol, next);
			
			next += _symbolOffset;
		}
	}
}