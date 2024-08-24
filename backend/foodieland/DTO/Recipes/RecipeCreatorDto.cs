namespace foodieland.DTO.Recipes;

public class RecipeCreatorDto
{
    public Guid CreatorId { get; set; }

    public string CreatorName { get; set; }

    public string? UserImage { get; set; }
}
