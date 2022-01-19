namespace wordleHelper;

/// <summary>
/// represents a line in the solutions grid.
/// </summary>
internal class Line
{
    /// <summary>
    /// Stores the characters in this line. Array (remember zero based index)
    /// dash means no-value here, we guard this by only manipulting this via SetChar
    /// </summary>
    private readonly char[] _chars = { '-', '-', '-', '-', '-' };

    /// <summary>
    /// Stores the Status of these our 5 fields in this line. Array (remember zero based index)
    /// </summary>
    private readonly FieldStatus[] _fieldColors =
    {
        FieldStatus.Unset,
        FieldStatus.Unset,
        FieldStatus.Unset,
        FieldStatus.Unset,
        FieldStatus.Unset
    };

    /// <summary>
    /// Sets a character in the given column. (0-based indeX)
    /// </summary>
    /// <param name="letter"></param>
    /// <param name="column">0-based column index</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void SetChar(char? letter, byte column)
    {
        if (column is >= 0 and < 5)
        {
            _chars[column] = letter ?? '-';
        }
        else
        {
            throw new ArgumentOutOfRangeException(paramName: nameof(column));
        }
    }

    /// <summary>
    /// /// Sets a status/color of the given column. (0-based indeX)
    /// </summary>
    /// <param name="fieldStatus">Status to set for the column</param>
    /// <param name="column">0-based column index</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void SetFieldStatus(FieldStatus fieldStatus, byte column)
    {
        if (column is >= 0 and < 5)
        {
            _fieldColors[column] = fieldStatus;
        }
        else
        {
            throw new ArgumentOutOfRangeException(paramName: nameof(column));
        }
    }

    public IEnumerable<char> GetUnavailableChars() =>
        _chars.Where(predicate: (t, i) => 
                _fieldColors[i] == FieldStatus.Gray && t != '-')
            .Select(selector: char.ToLower)
            .ToList();

    public IEnumerable<char> GetCharactersThatMustBeContained()
    {
        var retVal = new HashSet<char>();

        for (byte i = 0; i < _chars.Length; i++)
        {
            if (_fieldColors[i] is FieldStatus.Green or FieldStatus.Yellow && _chars[i] != '-') retVal.Add(item: char.ToLower(c: _chars[i]));
        }

        return retVal.AsEnumerable();
    }

    public IEnumerable<(byte pos, char c)> GetCharactersWithPositionsWhereTheyCannotBe(IEnumerable<(byte pos, char c)> mustPosForLine)
    {
        var mustPosChars = mustPosForLine.Select(selector: mp => mp.c).ToList();
        var retVal = new HashSet<(byte pos, char c)>();

        for (byte i = 0; i < _chars.Length; i++)
        {
            if (_fieldColors[i] is FieldStatus.Yellow && _chars[i] != '-') retVal.Add(item: (i, char.ToLower(c: _chars[i])));
        }

        for (byte i = 0; i < _chars.Length; i++)
        {
            if (_fieldColors[i] is FieldStatus.Gray && _chars[i] != '-' && mustPosChars.Contains(item: char.ToLower(c: _chars[i]))) retVal.Add(item: (i, char.ToLower(c: _chars[i])));
        }

        return retVal.AsEnumerable();
    }

    public IEnumerable<(byte pos, char c)> GetDefinitivePositions()
    {
        var retVal = new HashSet<(byte pos, char c)>();

        for (byte i = 0; i < _chars.Length; i++)
        {
            if (_fieldColors[i] is FieldStatus.Green && _chars[i] != '-') retVal.Add(item: (i, char.ToLower(c: _chars[i])));
        }
        return retVal.AsEnumerable();
    }
}