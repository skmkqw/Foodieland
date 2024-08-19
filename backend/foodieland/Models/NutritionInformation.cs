using System.Text.Json.Serialization;

namespace foodieland.Models;

public class NutritionInformation
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public double Calories { get; set; }
    
    public double Fat { get; set; }
    
    public double Protein { get; set; }
    
    public double Carbohydrate { get; set; }
    
    public Guid RecipeId { get; set; }
    
    [JsonIgnore]
    public Recipe Recipe { get; set; }
}