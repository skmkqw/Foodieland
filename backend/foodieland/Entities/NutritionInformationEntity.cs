using System.Text.Json.Serialization;

namespace foodieland.Entities;

public class NutritionInformationEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public double Calories { get; set; }
    
    public double Fat { get; set; }
    
    public double Protein { get; set; }
    
    public double Carbohydrate { get; set; }
    
    public double Cholesterol { get; set; }

    public Guid RecipeId { get; set; }
    
    [JsonIgnore]
    public RecipeEntity RecipeEntity { get; set; }
}