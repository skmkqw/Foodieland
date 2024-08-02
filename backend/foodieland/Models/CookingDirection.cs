using foodieland.DTO.CookingDirection;

namespace foodieland.Models;

public class CookingDirection
{
    public Guid Id { get; } = Guid.NewGuid();

    public byte StepNumber { get; private set; }
    
    public string Title { get; private set; }

    public string Description { get; private set; }

    public Recipe Recipe { get; }
    
    private CookingDirection(AddOrUpdateCookingDirectionDto createDirectionDto)
    {
        Title = createDirectionDto.Title;
        Description = createDirectionDto.Description;
        StepNumber = createDirectionDto.StepNumber;
    }

    public static CookingDirection Create(AddOrUpdateCookingDirectionDto createDirectionDto)
    {
        if (string.IsNullOrWhiteSpace(createDirectionDto.Title))
            throw new ArgumentException("Title cannot be empty", nameof(createDirectionDto.Title));
        
        if (createDirectionDto.Title.Length > 200)
            throw new ArgumentException("Name cannot exceed 200 characters", nameof(createDirectionDto.Title));
        
        if (string.IsNullOrWhiteSpace(createDirectionDto.Description))
            throw new ArgumentException("Description cannot be empty", nameof(createDirectionDto.Description));
        
        if (createDirectionDto.Description.Length > 500)
            throw new ArgumentException("Description cannot exceed 500 characters", nameof(createDirectionDto.Description));

        return new CookingDirection(createDirectionDto);
    }
}