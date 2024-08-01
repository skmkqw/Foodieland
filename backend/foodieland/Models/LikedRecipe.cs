namespace foodieland.Models;

public class LikedRecipe
{
        public Guid UserId { get; set; }
        public AppUser User { get; set; }

        public Guid RecipeId { get; set; }
        public Recipe Recipe { get; set; }
}