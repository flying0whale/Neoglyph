using System.Collections;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Neoglyph.Dictionary;

public class GlyphDictionary : IEnumerable<KeyValuePair<string, string>>
{
	private static readonly JsonSerializerOptions _options = new JsonSerializerOptions {
		Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
		WriteIndented = true
	};
	
	private readonly Dictionary<string, string> _glyphs;
	
	public Dictionary<string,string>.KeyCollection Keys => _glyphs.Keys;

	private GlyphDictionary(Dictionary<string, string> glyphs)
	{
		_glyphs = glyphs;
		_glyphs[" "] = " ";
	}

	public static GlyphDictionary FromDictionary(Dictionary<string, string> glyphs)
	{
		return new GlyphDictionary(glyphs);
	}

	public static GlyphDictionary FromFile(string path)
	{
		if (!File.Exists(path)) {
			Console.WriteLine($"File {path} does not exist.");
			return new GlyphDictionary([]);
		}
		
		var json = File.ReadAllText(path);
		if (string.IsNullOrWhiteSpace(json)) {
			Console.WriteLine($"File {path} is empty.");
			return new GlyphDictionary([]);
		}

		try {
			var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json)!;
			return new GlyphDictionary(dictionary);
		}
		catch (Exception e) {
			Console.WriteLine($"File {path} is not a valid json. Exception: {e.Message}");
		}
		
		return new GlyphDictionary([]);
	}

	public void ExportToFile(string path)
	{
		var json = JsonSerializer.Serialize(_glyphs, _options);
		File.WriteAllText(path, json);
	}

	public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
	{
		return _glyphs.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
	
	public string this[string c] => _glyphs[c];
}