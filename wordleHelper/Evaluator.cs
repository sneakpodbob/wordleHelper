namespace wordleHelper;

public class Evaluator
{
    private readonly List<char> _availables = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower().ToCharArray().ToList();
    private readonly Field _field = new();

    public async Task<List<string>> EvaluateAsync(bool sort)
    {
        var list = await Wordlist.CreateInstanceAsync();

        // Apply all sorts of filters here.

        // available chars 
        var unavailableChars = _field.GetUnavailableChars().ToList();

        // Characters That Must Be Contained (greens and yellows)
        var charactersThatMustBeContained = _field.GetCharactersThatMustBeContained();

        // Characters where we definitively know where they are (greens)
        var definitivePositions = _field.GetDefinitivePositions();

        // cannot pos
        var charactersWithPositionsWhereTheyCannotBe = _field.GetCharactersWithPositionsWhereTheyCannotBe();

        for (var i = unavailableChars.Count - 1; i >= 0; i--)
        {
            if (definitivePositions.Exists(pos => pos.c == unavailableChars[i]))
            {
                unavailableChars.RemoveAt(i);
            }
        }

        var availableChars = _availables.Where(c => !unavailableChars.Contains(c));

        bool PredMustPos(string word)
        {
            if (definitivePositions.Count == 0) return true;

            var ret = true;
            foreach (var (pos, buchstabe) in definitivePositions)
            {
                ret &= word.Substring(pos, 1) == buchstabe.ToString();
            }

            return ret;
        }

        bool PredAvailChars(string c) => c.ToCharArray().All(value => availableChars.Contains(value));

        bool PredMustContainChars(string word) => charactersThatMustBeContained.Length == 0 || charactersThatMustBeContained.All(must => word.ToCharArray().Contains(must));

        bool PredCannotPos(string word)
        {
            if (charactersWithPositionsWhereTheyCannotBe.Count == 0) return true;

            var ret = true;
            foreach (var (pos, buchstabe) in charactersWithPositionsWhereTheyCannotBe)
            {
                ret &= word.Substring(pos, 1) != buchstabe.ToString();
            }

            return ret;
        }

        var returnList = list.AsParallel().AsOrdered()
            .Where(PredAvailChars)
            .Where(PredMustContainChars)
            .Where(PredMustPos)
            .Where(PredCannotPos);

        return sort ? returnList.OrderByDescending(GetSortPointsForWord).ToList() : returnList.ToList();
    }

    public void SetConditionGreen(byte line, byte column)
    {
        _field.SetCondition(FieldColor.Green, line, column);
    }

    public void SetConditionYellow(byte line, byte column)
    {
        _field.SetCondition(FieldColor.Yellow, line, column);
    }

    public void SetConditionGray(byte line, byte column)
    {
        _field.SetCondition(FieldColor.Gray, line, column);
    }

    public void UnSetCondition(byte line, byte column)
    {
        _field.SetCondition(FieldColor.Unset, line, column);
    }

    public void SetChar(char? c, byte line, byte column)
    {
        _field.SetChar(c, line, column);
    }

    private static decimal GetSortPointsForWord(string word)
    {
        var pointsDict = new Dictionary<char, decimal>
        {
            { 'e', 11.16m },
            { 'a', 8.45m },
            { 'r', 7.58m },
            { 'i', 7.54m },
            { 'o', 7.16m },
            { 't', 6.95m },
            { 'n', 6.65m },
            { 's', 5.74m },
            { 'l', 5.49m },
            { 'c', 4.54m },
            { 'u', 3.63m },
            { 'd', 3.38m }
        };

        var points = 0m;
        var seen = new List<char>();

        foreach (var c in word.ToCharArray())
        {
            // Strafpunkte für dopplte Buchstaben
            if (seen.Contains(c))
            {
                points -= 8;
            }
            seen.Add(c);

            if (!pointsDict.ContainsKey(c)) continue;

            points += pointsDict[c];
            pointsDict.Remove(c);
        }

        return points;
    }
}