namespace wordleHelper;

/// <summary>
/// represents the solutions grid
/// </summary>
internal class Field
{
    /// <summary>
    /// Stores our 5 line objects.
    /// </summary>
    private readonly Line[] _lines = { new(), new(), new(), new(), new() };

    /// <summary>
    /// Sets the letter for the given coordinates.
    /// 1-based indexing.
    /// </summary>
    /// <param name="letter">Letter to set for the given cell.</param>
    /// <param name="line">Line Number (1-based index)</param>
    /// <param name="column">Column Number (1-based index)</param>
    public void SetChar(char? letter, byte line, byte column)
    {
        _lines[line - 1].SetChar(letter: letter, column: (byte)(column - 1));
    }

    /// <summary>
    /// Sets the Status/Color for the given Coordinates
    /// 1-based indexing.
    /// </summary>
    /// <param name="fieldStatus">Status/Color to set for the given cell.</param>
    /// <param name="line">Line Number (1-based index)</param>
    /// <param name="column">Column Number (1-based index)</param>
    public void SetCondition(FieldStatus fieldStatus, byte line, byte column)
    {
        _lines[line - 1].SetFieldStatus(fieldStatus: fieldStatus, column: (byte)(column - 1));
    }

    /// <summary>
    /// Returns an Enumerable of Letters that are definitely NOT in the target word.
    /// </summary>
    /// <returns>Collection of Chars that are unavailable for the target word</returns>
    public IEnumerable<char> GetUnavailableChars()
    {
        var ret = new HashSet<char>();
        foreach (var line in _lines)
        {
            foreach (var unavailableChar in line.GetUnavailableChars())
            {
                ret.Add(item: unavailableChar);
            }
        }

        return ret.ToArray();
    }

    /// <summary>
    /// Returns an Enumerable of Letters that are definitely IN the target word.
    /// </summary>
    /// <returns>Collection of Chars that are IN the target word for sure</returns>
    public char[] GetCharactersThatMustBeContained()
    {
        var ret = new HashSet<char>();
        foreach (var line in _lines)
        {
            foreach (var mustBeContained in line.GetCharactersThatMustBeContained())
            {
                ret.Add(item: mustBeContained);
            }
        }

        return ret.ToArray();
    }

    /// <summary>
    /// Returns a List of Letters and their Position in the target word, where we know that they ARE NOT.
    /// </summary>
    /// <returns>List of Tuple - position in the word and letter - 0-based index.</returns>
    public List<(byte pos, char c)> GetCharactersWithPositionsWhereTheyCannotBe()
    {
        var ret = new HashSet<(byte pos, char c)>();

        foreach (var line in _lines)
        {
            var mustPosForLine = line.GetDefinitivePositions().ToList();
            foreach (var cannotBeTherePos in line.GetCharactersWithPositionsWhereTheyCannotBe(mustPosForLine: mustPosForLine))
            {
                ret.Add(item: cannotBeTherePos);
            }
        }

        return ret.ToList();
    }

    /// <summary>
    /// Returns a List of Letters and their Position in the target word, where we know that they ARE positioned.
    /// </summary>
    /// <returns>List of Tuple - position in the word and letter - 0-based index.</returns>
    public List<(byte pos, char c)> GetDefinitivePositions()
    {
        var ret = new HashSet<(byte pos, char c)>();

        foreach (var line in _lines)
        {
            foreach (var definitivePosition in line.GetDefinitivePositions())
            {
                ret.Add(item: definitivePosition);
            }
        }

        return ret.ToList();
    }

}