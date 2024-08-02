namespace foodieland.Entities;

public class IngredientEntity
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public List<IngredientQuantityEntity> IngredientQuantities { get; set; }
}