using Backend.Domain.Entities;

namespace Backend.Dtos;

public class RecipeSuggestionDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int MatchCount { get; set; }
    public int TotalRequired { get; set; }
    public int MissingCount => TotalRequired - MatchCount;
    public bool IsFullMatched => MatchCount == TotalRequired;
}

