/*
 *  wordleHelper - A little Windows-App to cheat when playing wordle.
    Copyright (C) 2022 Robert Krüger

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

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