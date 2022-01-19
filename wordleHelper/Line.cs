namespace wordleHelper;

internal class Line
{
    private char[] Chars { get; } = { '-', '-', '-', '-', '-' };

    private FieldColor[] FieldColors { get; } =
    {
        FieldColor.Unset,
        FieldColor.Unset,
        FieldColor.Unset,
        FieldColor.Unset,
        FieldColor.Unset
    };

    public void SetChar(char? c, byte column)
    {
        if (column is >= 0 and < 5)
        {
            Chars[column] = c ?? '-';
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(column));
        }
    }

    public void SetFieldColor(FieldColor fc, byte column)
    {
        if (column is >= 0 and < 5)
        {
            FieldColors[column] = fc;
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(column));
        }
    }

    public IEnumerable<char> GetUnavailableChars() =>
        Chars.Where((t, i) => 
                FieldColors[i] == FieldColor.Gray && t != '-')
            .Select(char.ToLower)
            .ToList();

    public IEnumerable<char> GetCharactersThatMustBeContained()
    {
        var retVal = new HashSet<char>();

        for (byte i = 0; i < Chars.Length; i++)
        {
            if (FieldColors[i] is FieldColor.Green or FieldColor.Yellow && Chars[i] != '-') retVal.Add(char.ToLower(Chars[i]));
        }

        return retVal.AsEnumerable();
    }

    public IEnumerable<(byte pos, char c)> GetCharactersWithPositionsWhereTheyCannotBe(IEnumerable<(byte pos, char c)> mustPosForLine)
    {
        var mustPosChars = mustPosForLine.Select(mp => mp.c).ToList();
        var retVal = new HashSet<(byte pos, char c)>();

        for (byte i = 0; i < Chars.Length; i++)
        {
            if (FieldColors[i] is FieldColor.Yellow && Chars[i] != '-') retVal.Add((i, char.ToLower(Chars[i])));
        }

        for (byte i = 0; i < Chars.Length; i++)
        {
            if (FieldColors[i] is FieldColor.Gray && Chars[i] != '-' && mustPosChars.Contains(char.ToLower(Chars[i]))) retVal.Add((i, char.ToLower(Chars[i])));
        }

        return retVal.AsEnumerable();
    }

    public IEnumerable<(byte pos, char c)> GetDefinitivePositions()
    {
        var retVal = new HashSet<(byte pos, char c)>();

        for (byte i = 0; i < Chars.Length; i++)
        {
            if (FieldColors[i] is FieldColor.Green && Chars[i] != '-') retVal.Add((i, char.ToLower(Chars[i])));
        }
        return retVal.AsEnumerable();
    }
}