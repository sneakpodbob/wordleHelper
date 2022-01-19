namespace wordleHelper;

internal class Wordlist : List<string>
{
    private static Wordlist? _instance;

    private Wordlist()
    {
        var lines = File.ReadAllLines("words_alpha.txt");
        AddRange(lines.AsParallel().Where(s => s.Length == 5).Select(s => s.ToLower()).ToList());
    }

    public static Wordlist CreateInstance()
    {
        return _instance ??= new Wordlist();
    }
}