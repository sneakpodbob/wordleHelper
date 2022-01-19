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