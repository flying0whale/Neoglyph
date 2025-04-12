using Neoglyph.Dictionary;

namespace Neoglyph.Glyph;

public class GlyphParser(GlyphDictionary dictionary)
{
	public IEnumerable<Word> ParseText(string original)
	{
		var words = original.Split(' ');

		foreach (var word in words) {
			List<Glyph> glyphs = [];
			
			foreach (var c in word) {
				glyphs.Add(new Glyph(dictionary[c.ToString()]));
			}
			
			yield return new Word(glyphs.ToArray());
		}
	}
}