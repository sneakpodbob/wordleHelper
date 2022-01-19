namespace wordleHelper;

/// <summary>
/// represents our wordlist in memory
/// only 5-letter word contained
/// is read from a file named words.txt - one word each line
/// file must be in the executables directory
/// </summary>
internal class Wordlist : List<string>
{
    // Let's make this a singleton, so we don't re-read the file.
    private static Wordlist? _instance;

    // read the file line by line and only use 5-letter words.
    private async Task InitializeAsync()
    {
        var lines = await File.ReadAllLinesAsync(path: "words.txt");
        AddRange(collection: lines.AsParallel().Where(predicate: s => s.Length == 5).Select(selector: s => s.ToLower()).ToList());
    }

    /// <summary>
    /// Get's the singleton instance for the wordlist
    /// </summary>
    /// <returns>A task to await the wordlist</returns>
    public static async Task<Wordlist> CreateInstanceAsync()
    {
        if (_instance != null) return _instance;
        
        _instance = new Wordlist();
        await _instance.InitializeAsync();
        return _instance;
    }
}