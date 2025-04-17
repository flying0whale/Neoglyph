using Neoglyph.Graphics;

namespace Neoglyph.Glyph;

public class GlyphRenderer(Canvas canvas)
{
	private static readonly Cell _symbolOffset = new Cell(1, 0);
	private static readonly Cell _glyphOffset  = new Cell(0.5, 0.5);
	private static readonly Cell _wordOffset   = new Cell(0, 1);

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
		for (var i = 0; i < word.Glyphs.Length; i++) {
			RenderGlyph(word, next, i);
			
			next += _glyphOffset;
		}
	}

	public void RenderGlyph(Word context, Cell start, int glyphIndex)
	{
		var next = start;
		var glyph = context.Glyphs[glyphIndex];
		var nextGlyph = context.Glyphs.ElementAtOrDefault(glyphIndex + 1);
		for (var i = 0; i < context.Glyphs[0].Symbols.Count; i++) {
			if (nextGlyph.Symbols != null) {
				SmoothRenderGlyph(context, next, glyphIndex, i);
			}

			canvas.DrawSymbol(glyph.Symbols[i], next);
			next += _symbolOffset;
		}
	}

	private void SmoothRenderGlyph(Word context, Cell cell, int glyphIndex, int symbolIndex)
	{
		if (glyphIndex == context.Glyphs.Length - 1) {
			return;
		}
		
		var currentSymbol = context.Glyphs[glyphIndex].Symbols[symbolIndex];
		
		var smoothRightDown = SmoothRightDown(context, currentSymbol, glyphIndex, symbolIndex);
		if (smoothRightDown) {
			canvas.DrawArc(Sectors.TopRight, cell + new Cell(0.5, 1));
		}
		
		var smoothLeftDown = SmoothLeftDown(context, currentSymbol, glyphIndex, symbolIndex);
		if (smoothLeftDown) {
			canvas.DrawArc(Sectors.TopLeft, cell + new Cell(0.5, 1));
		}
		
		var smoothBottomRight = SmoothBottomRight(context, currentSymbol, glyphIndex, symbolIndex);
		if (smoothBottomRight) {
			canvas.DrawArc(Sectors.BottomLeft, cell + new Cell(1, 0.5));
		}
		
		var smoothBottomLeft = SmoothBottomLeft(context, currentSymbol, glyphIndex, symbolIndex);
		if (smoothBottomLeft) {
			canvas.DrawArc(Sectors.BottomRight, cell + new Cell(0, 0.5));
		}
	}

	private static bool SmoothBottomLeft(Word context, Glyph.Part[] currentSymbol, int glyphIndex, int symbolIndex)
	{
		if (symbolIndex == 0) {
			return false;
		}
		
		if (currentSymbol[3] == Glyph.Part.None) {
			return false;
		}
		
		var respectiveSymbol = context.Glyphs[glyphIndex + 1].Symbols[symbolIndex - 1];
		if (respectiveSymbol[2] == Glyph.Part.None) {
			return false;
		}
		
		var checkSymbol = context.Glyphs[glyphIndex + 1].Symbols.ElementAtOrDefault(symbolIndex);
		if (checkSymbol is not null && checkSymbol[0] == Glyph.Part.Line) {
			return false;
		}
		
		if (glyphIndex < context.Glyphs.Length - 2) {
			checkSymbol = context.Glyphs[glyphIndex + 2].Symbols[symbolIndex - 1];
			if (checkSymbol[1] == Glyph.Part.Line) {
				return false;
			}
		}

		currentSymbol[3] = Glyph.Part.None;
		respectiveSymbol[2] = Glyph.Part.None;
		
		return true;
	}

	private static bool SmoothBottomRight(Word context, Glyph.Part[] currentSymbol, int glyphIndex, int symbolIndex)
	{
		if (currentSymbol[3] == Glyph.Part.None) {
			return false;
		}

		if (symbolIndex != 0) {
			var nextPrevSymbol = context.Glyphs[glyphIndex + 1].Symbols[symbolIndex - 1];
			if (nextPrevSymbol[2] == Glyph.Part.Line) {
				return false;
			}
			
			if (glyphIndex < context.Glyphs.Length - 2) {
				var checkSymbol = context.Glyphs[glyphIndex + 2].Symbols[symbolIndex - 1];
				if (checkSymbol[1] == Glyph.Part.Line) {
					return false;
				}
			}
		}

		if (glyphIndex < context.Glyphs.Length - 2 && symbolIndex != 0) {
			var checkSymbol = context.Glyphs[glyphIndex + 2].Symbols[symbolIndex - 1];
			if (checkSymbol[1] == Glyph.Part.Line) {
				return false;
			}
		}
		
		var respectiveSymbol = context.Glyphs[glyphIndex + 1].Symbols[symbolIndex];
		if (respectiveSymbol[0] == Glyph.Part.None) {
			return false;
		}

		currentSymbol[3] = Glyph.Part.None;
		respectiveSymbol[0] = Glyph.Part.None;
		
		return true;
	}
	
	private static bool SmoothLeftDown(Word context, Glyph.Part[] currentSymbol, int glyphIndex, int symbolIndex)
	{
		if (symbolIndex == 0) {
			return false;
		}
		
		if (currentSymbol[0] == Glyph.Part.None) {
			return false;
		}
		
		var respectiveSymbol = context.Glyphs[glyphIndex + 1].Symbols[symbolIndex - 1];
		if (respectiveSymbol[1] == Glyph.Part.None) {
			return false;
		}

		if (glyphIndex != 0) {
			var prevGlyphSymbol = context.Glyphs[glyphIndex - 1].Symbols[symbolIndex];
			if (prevGlyphSymbol[3] == Glyph.Part.Line) {
				return false;
			}
		}
		
		var prevSymbol = context.Glyphs[glyphIndex].Symbols[symbolIndex - 1];
		if (prevSymbol[2] == Glyph.Part.Line) {
			return false;
		}

		currentSymbol[0] = Glyph.Part.None;
		respectiveSymbol[1] = Glyph.Part.None;
		
		return true;
	}
	
	private static bool SmoothRightDown(Word context, Glyph.Part[] currentSymbol, int glyphIndex, int symbolIndex)
	{
		if (currentSymbol[2] != Glyph.Part.Line) {
			return false;
		}
		
		var nextSymbol = context.Glyphs[glyphIndex].Symbols.ElementAtOrDefault(symbolIndex + 1);
		if (nextSymbol is not null && nextSymbol[0] == Glyph.Part.Line) {
			return false;
		}
		
		var respectiveSymbol = context.Glyphs[glyphIndex + 1].Symbols[symbolIndex];
		if (respectiveSymbol[1] != Glyph.Part.Line) return false;
		
		if (glyphIndex != 0) {
			var prevGlyphNextSymbol = context.Glyphs[glyphIndex - 1].Symbols.ElementAtOrDefault(symbolIndex + 1);

			if (prevGlyphNextSymbol is not null && prevGlyphNextSymbol[3] == Glyph.Part.Line) {
				return false;
			}
		}
		
		currentSymbol[2] = Glyph.Part.None;
		respectiveSymbol[1] = Glyph.Part.None;

		return true;
	}
}