using System.Text.Json.Serialization;

namespace foodieland.Entities;

public class CookingDirectionEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public byte StepNumber { get; set; }
    
    public string Title { get; set; } = string.Empty;

    public string Descirption { get; set; } = string.Empty;

    public Guid RecipeId { get; set; }
    
    [JsonIgnore]
    public RecipeEntity RecipeEntity { get; set; }
}