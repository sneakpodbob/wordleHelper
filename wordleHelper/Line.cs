namespace wordleHelper;

internal class Line
{
    public char? C1 { get; set; }
    public char? C2 { get; set; }
    public char? C3 { get; set; }
    public char? C4 { get; set; }
    public char? C5 { get; set; }

    public FieldColor F1 { get; set; }
    public FieldColor F2 { get; set; }
    public FieldColor F3 { get; set; }
    public FieldColor F4 { get; set; }
    public FieldColor F5 { get; set; }

    public IEnumerable<char> GetUnavailableChars()
    {
        var retVal = new List<char>();
        if (F1 == FieldColor.Gray && C1 is not null) retVal.Add(char.ToLower(C1.Value));
        if (F2 == FieldColor.Gray && C2 is not null) retVal.Add(char.ToLower(C2.Value));
        if (F3 == FieldColor.Gray && C3 is not null) retVal.Add(char.ToLower(C3.Value));
        if (F4 == FieldColor.Gray && C4 is not null) retVal.Add(char.ToLower(C4.Value));
        if (F5 == FieldColor.Gray && C5 is not null) retVal.Add(char.ToLower(C5.Value));
        return retVal.AsEnumerable();
    }

    public IEnumerable<char> GetMustContains()
    {
        var retVal = new HashSet<char>();
        if (F1 is FieldColor.Green or FieldColor.Yellow && C1 is not null) retVal.Add(char.ToLower(C1.Value));
        if (F2 is FieldColor.Green or FieldColor.Yellow && C2 is not null) retVal.Add(char.ToLower(C2.Value));
        if (F3 is FieldColor.Green or FieldColor.Yellow && C3 is not null) retVal.Add(char.ToLower(C3.Value));
        if (F4 is FieldColor.Green or FieldColor.Yellow && C4 is not null) retVal.Add(char.ToLower(C4.Value));
        if (F5 is FieldColor.Green or FieldColor.Yellow && C5 is not null) retVal.Add(char.ToLower(C5.Value));
        return retVal.AsEnumerable();
    }

    public IEnumerable<(byte pos, char c)> GetCannotPos(List<(byte pos, char c)> mustPosForLine)
    {
        var mustPosChars = mustPosForLine.Select(mp => mp.c).ToList();
        var retVal = new HashSet<(byte pos, char c)>();
        if (F1 is FieldColor.Yellow && C1 is not null) retVal.Add((0, char.ToLower(C1.Value)));
        if (F2 is FieldColor.Yellow && C2 is not null) retVal.Add((1, char.ToLower(C2.Value)));
        if (F3 is FieldColor.Yellow && C3 is not null) retVal.Add((2, char.ToLower(C3.Value)));
        if (F4 is FieldColor.Yellow && C4 is not null) retVal.Add((3, char.ToLower(C4.Value)));
        if (F5 is FieldColor.Yellow && C5 is not null) retVal.Add((4, char.ToLower(C5.Value)));

        if (F1 is FieldColor.Gray && C1 is not null && mustPosChars.Contains(char.ToLower(C1.Value))) retVal.Add((0, char.ToLower(C1.Value)));
        if (F2 is FieldColor.Gray && C2 is not null && mustPosChars.Contains(char.ToLower(C2.Value))) retVal.Add((1, char.ToLower(C2.Value)));
        if (F3 is FieldColor.Gray && C3 is not null && mustPosChars.Contains(char.ToLower(C3.Value))) retVal.Add((2, char.ToLower(C3.Value)));
        if (F4 is FieldColor.Gray && C4 is not null && mustPosChars.Contains(char.ToLower(C4.Value))) retVal.Add((3, char.ToLower(C4.Value)));
        if (F5 is FieldColor.Gray && C5 is not null && mustPosChars.Contains(char.ToLower(C5.Value))) retVal.Add((4, char.ToLower(C5.Value)));

        return retVal.AsEnumerable();
    }

    public IEnumerable<(byte pos, char c)> GetMustPos()
    {
        var retVal = new HashSet<(byte pos, char c)>();
        if (F1 is FieldColor.Green && C1 is not null) retVal.Add((0, char.ToLower(C1.Value)));
        if (F2 is FieldColor.Green && C2 is not null) retVal.Add((1, char.ToLower(C2.Value)));
        if (F3 is FieldColor.Green && C3 is not null) retVal.Add((2, char.ToLower(C3.Value)));
        if (F4 is FieldColor.Green && C4 is not null) retVal.Add((3, char.ToLower(C4.Value)));
        if (F5 is FieldColor.Green && C5 is not null) retVal.Add((4, char.ToLower(C5.Value)));
        return retVal.AsEnumerable();
    }
}