using foodieland.Models;

namespace foodieland.Entities;

public class LikedRecipeEntity
{
        public Guid UserId { get; set; }
        public AppUser User { get; set; }

        public Guid RecipeId { get; set; }
        public RecipeEntity RecipeEntity { get; set; }
}