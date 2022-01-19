namespace wordleHelper;

internal class Field
{
    private readonly Line[] _lines = { new(), new(), new(), new(), new() };

    public void SetChar(char? c, byte line, byte column)
    {
        _lines[line - 1].SetChar(c, (byte)(column - 1));
    }

    public void SetCondition(FieldColor fieldColor, byte line, byte column)
    {
        _lines[line - 1].SetFieldColor(fieldColor, (byte)(column - 1));
    }

    public IEnumerable<char> GetUnavailableChars()
    {
        var ret = new HashSet<char>();
        foreach (var line in _lines)
        {
            foreach (var unavailableChar in line.GetUnavailableChars())
            {
                ret.Add(unavailableChar);
            }
        }

        return ret.ToArray();
    }

    public char[] GetCharactersThatMustBeContained()
    {
        var ret = new HashSet<char>();
        foreach (var line in _lines)
        {
            foreach (var mustBeContained in line.GetCharactersThatMustBeContained())
            {
                ret.Add(mustBeContained);
            }
        }

        return ret.ToArray();
    }

    public List<(byte pos, char c)> GetCharactersWithPositionsWhereTheyCannotBe()
    {
        var ret = new HashSet<(byte pos, char c)>();

        foreach (var line in _lines)
        {
            var mustPosForLine = line.GetDefinitivePositions().ToList();
            foreach (var cannotBeTherePos in line.GetCharactersWithPositionsWhereTheyCannotBe(mustPosForLine))
            {
                ret.Add(cannotBeTherePos);
            }
        }

        return ret.ToList();
    }

    public List<(byte pos, char c)> GetDefinitivePositions()
    {
        var ret = new HashSet<(byte pos, char c)>();

        foreach (var line in _lines)
        {
            foreach (var definitivePosition in line.GetDefinitivePositions())
            {
                ret.Add(definitivePosition);
            }
        }

        return ret.ToList();
    }

}