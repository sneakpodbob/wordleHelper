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