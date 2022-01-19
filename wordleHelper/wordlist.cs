namespace wordleHelper;

internal class Wordlist : List<string>
{
    private static Wordlist? _instance;

    private async Task InitializeAsync()
    {
        var lines = await File.ReadAllLinesAsync("words_alpha.txt");
        AddRange(lines.AsParallel().Where(s => s.Length == 5).Select(s => s.ToLower()).ToList());
    }

    public static async Task<Wordlist> CreateInstanceAsync()
    {
        if (_instance != null) return _instance;
        
        _instance = new Wordlist();
        await _instance.InitializeAsync();
        return _instance;
    }
}