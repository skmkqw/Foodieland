namespace foodieland.DTO.CookingDirection;

public class CookingDirectionDto
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public byte StepNumber { get; set; }
    
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Guid RecipeId { get; set; }
}