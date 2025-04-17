using Neoglyph.Dictionary;
using Neoglyph.Glyph;
using Neoglyph.Graphics;

var dictionary = GlyphDictionary.FromFile("../../../Dictionary/english.json");
var writer = new GlyphParser(dictionary);

var text = writer.ParseText("all human beings are born free and equal in dignity and rights");

Canvas svgCanvas = new SvgCanvas(20, 100, 50,
					  Colors.Elegie,
					  Colors.RainyDay);
var renderer = new GlyphRenderer(svgCanvas);

renderer.RenderText(text.ToList(), new Cell(1, 1));
svgCanvas.SaveToFile("../../../neoglyph.svg");