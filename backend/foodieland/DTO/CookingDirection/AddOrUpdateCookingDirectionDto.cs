namespace foodieland.DTO.CookingDirection;

public class AddOrUpdateCookingDirectionDto
{
    public byte StepNumber { get; set; }
    
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

}