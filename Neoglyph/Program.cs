using Neoglyph.Glyphs;

const string PATH = "../../../Dictionary/english.json";
var glyphs = GlyphDictionary.FromFile(PATH);

foreach (var pair in glyphs) {
	Console.WriteLine($"{pair.Key}: {pair.Value}");
}