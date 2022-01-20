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
/// Evaluator takes all the status via SetChar and SetFieldstatus
/// and then returns a list of possible Solutions to the wordle via EvaluateAsync
/// </summary>
public class Evaluator
{
    private readonly List<char> _validAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower().ToCharArray().ToList();
    private readonly Field _field = new();

    /// <summary>
    /// Evaluates all known informations about the target word and return a list of possible
    /// solutions to the puzzle.
    /// </summary>
    /// <param name="sort">Sort so that words that contain more popular letters end up at the top of the list</param>
    /// <returns></returns>
    public async Task<List<string>> EvaluateAsync(bool sort)
    {
        // Get the wordlist
        var list = await Wordlist.CreateInstanceAsync();

        /*
         * Apply all sorts of filters here.
         */

        // available chars 
        var unavailableChars = _field.GetUnavailableChars().ToList();

        // Characters That Must Be Contained (greens and yellows)
        var charactersThatMustBeContained = _field.GetCharactersThatMustBeContained();

        // Characters where we definitively know where they are (greens)
        var definitivePositions = _field.GetDefinitivePositions();

        // cannot pos
        var charactersWithPositionsWhereTheyCannotBe = _field.GetCharactersWithPositionsWhereTheyCannotBe();

        // if you use a word that contains specific a letter more than once, you'll get your green/yellow indicator
        // only on the first one - so the second one (gray) would be in unavailableChars-Array - which is not correct
        // so we substract it here -- finding a more clever solution here would be a thing to consider.
        for (var i = unavailableChars.Count - 1; i >= 0; i--)
        {
            if (definitivePositions.Exists(match: pos => pos.c == unavailableChars[index: i]))
            {
                unavailableChars.RemoveAt(index: i);
            }
        }

        // filter the alphabet for chars that are still in the game
        var availableChars = _validAlphabet.Where(predicate: c => !unavailableChars.Contains(item: c));

        /*
         * predicates to use in the filtering.
         */
        bool PredMustPos(string word)
        {
            if (definitivePositions.Count == 0) return true;

            var ret = true;
            foreach (var (pos, buchstabe) in definitivePositions)
            {
                ret &= word.Substring(startIndex: pos, length: 1) == buchstabe.ToString();
            }

            return ret;
        }

        bool PredAvailChars(string c) => c.ToCharArray().All(predicate: value => availableChars.Contains(value: value));

        bool PredMustContainChars(string word) => charactersThatMustBeContained.Length == 0 || charactersThatMustBeContained.All(predicate: must => word.ToCharArray().Contains(value: must));

        bool PredCannotPos(string word)
        {
            if (charactersWithPositionsWhereTheyCannotBe.Count == 0) return true;

            var ret = true;
            foreach (var (pos, buchstabe) in charactersWithPositionsWhereTheyCannotBe)
            {
                ret &= word.Substring(startIndex: pos, length: 1) != buchstabe.ToString();
            }

            return ret;
        }

        var returnList = list.AsParallel().AsOrdered()
            .Where(predicate: PredAvailChars)
            .Where(predicate: PredMustContainChars)
            .Where(predicate: PredMustPos)
            .Where(predicate: PredCannotPos);

        // return the list, either sorted or as they've come from the wordlist (which is alphabetical)
        return sort ? returnList.OrderByDescending(keySelector: GetSortPointsForWord).ToList() : returnList.ToList();
    }

    /// <summary>
    /// Set the Color/Status-Information for the field with the given
    /// coordinates.
    /// </summary>
    /// <param name="color">Either Yellow, Green, White or Gray</param>
    /// <param name="line">line index (1-based)</param>
    /// <param name="column">column index (1-based)</param>
    /// <exception cref="ArgumentOutOfRangeException">if Color is not one of the 3 expected values</exception>
    public void SetColor(Color color, byte line, byte column)
    {
        _field.SetCondition(fieldStatus: GetFieldStatusFromColor(color: color), line: line, column: column);
    }

    /// <summary>
    /// Helper-Method to convert Color to Fieldstatus-Enum
    /// </summary>
    /// <param name="color">Either Yellow, Green, White or Gray</param>
    /// <returns>Fieldstatus Value that corresponds to InputColor</returns>
    /// <exception cref="ArgumentOutOfRangeException">if Color is not one of the 3 expected values</exception>
    private static FieldStatus GetFieldStatusFromColor(Color color)
    {
        var nextColorDict = new Dictionary<Color, FieldStatus>
        {
            {Color.Gray, FieldStatus.Gray},
            {Color.Yellow, FieldStatus.Yellow},
            {Color.Green, FieldStatus.Green},
            {Color.White, FieldStatus.Unset}
        };

        if (!nextColorDict.ContainsKey(key: color))
        {
            throw new ArgumentOutOfRangeException(paramName: nameof(color), actualValue: color, message: null);
        }

        return nextColorDict[key: color];
    }

    /// <summary>
    /// Set the Letter for the field with the given coordinates.
    /// </summary>
    /// <param name="letter">The users guessed letter</param>
    /// <param name="line">line index (1-based)</param>
    /// <param name="column">column index (1-based)</param>
    public void SetChar(char? letter, byte line, byte column)
    {
        _field.SetChar(letter: letter, line: line, column: column);
    }

    /// <summary>
    /// Helper - generates a number that is higher the more
    /// the word contains often used letters - double or triple letters decreases the number.
    /// </summary>
    /// <param name="word">word to judge</param>
    /// <returns>Points for sorting the word. Sort descending with this</returns>
    private static decimal GetSortPointsForWord(string word)
    {
        // Anteile der Buchstaben in der englischen Sprache nach Oxford 
        // beliebteste 12 Buchstaben
        // die nehmen wir einfach so als Punkte, andere Buchstaben kriegen 0 Punkte.
        Dictionary<char, decimal> pointsDict = new()
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
        var seen = new HashSet<char>();

        // so viele Punkte ziehen wir einem Wort ab das doppelte Buchstaben enthält
        // erstmal aus'm Bauch raus festgelegt
        const int pointsMalusForRepeatedLetters = 8;

        foreach (var c in word.ToCharArray())
        {
            // Strafpunkte für dopplte Buchstaben
            // (add returns false if the letter has already been seen before)
            if (!seen.Add(item: c))
            {
                points -= pointsMalusForRepeatedLetters;
            }

            if (!pointsDict.ContainsKey(key: c)) continue;

            points += pointsDict[key: c];
            pointsDict.Remove(key: c);
        }

        return points;
    }
}