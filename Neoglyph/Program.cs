using Neoglyph.Dictionary;
using Neoglyph.Glyph;
using Neoglyph.Graphics;

const string TEXT = "all human beings are born free and equal in dignity and rights";

var dictionary = GlyphDictionary.FromFile("../../../Dictionary/english.json");
var writer = new GlyphParser(dictionary);

var words = writer.ParseText(TEXT).ToList();

var height = TEXT.Length - words.Count;

Canvas svgCanvas = new SvgCanvas(20, height, 50,
					  Colors.Elegie,
					  Colors.RainyDay);
var renderer = new GlyphRenderer(svgCanvas);

renderer.RenderText(words, new Cell(1, 1));
svgCanvas.SaveToFile("../../../neoglyph.svg");