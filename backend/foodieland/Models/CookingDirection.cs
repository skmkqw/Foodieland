using System.Text.Json.Serialization;

namespace foodieland.Models;

public class CookingDirection
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public byte StepNumber { get; set; }
    
    public string Title { get; set; } = string.Empty;

    public string Descirption { get; set; } = string.Empty;

    public Guid RecipeId { get; set; }
    
    [JsonIgnore]
    public Recipe Recipe { get; set; }
}