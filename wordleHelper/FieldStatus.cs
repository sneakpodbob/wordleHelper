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
/// Represents the status of Field in the SolutionsGrid
/// </summary>
internal enum FieldStatus : byte
{
    Unset = 0,
    Gray = 1,
    Yellow = 2,
    Green = 3
}