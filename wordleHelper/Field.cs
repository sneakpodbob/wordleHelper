namespace wordleHelper;

internal class Field
{
    private readonly Line[] _lines = { new(), new(), new(), new(), new() };

    public void SetChar(char? c, byte line, byte row)
    {
        switch (row)
        {
            case 1:
                _lines[line - 1].C1 = c;
                break;
            case 2:
                _lines[line - 1].C2 = c;
                break;
            case 3:
                _lines[line - 1].C3 = c;
                break;
            case 4:
                _lines[line - 1].C4 = c;
                break;
            case 5:
                _lines[line - 1].C5 = c;
                break;
        }
    }

    public void SetCondition(FieldColor fieldColor, byte line, byte row)
    {
        switch (row)
        {
            case 1:
                _lines[line - 1].F1 = fieldColor;
                break;
            case 2:
                _lines[line - 1].F2 = fieldColor;
                break;
            case 3:
                _lines[line - 1].F3 = fieldColor;
                break;
            case 4:
                _lines[line - 1].F4 = fieldColor;
                break;
            case 5:
                _lines[line - 1].F5 = fieldColor;
                break;
        }
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