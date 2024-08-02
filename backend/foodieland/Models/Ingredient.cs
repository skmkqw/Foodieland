namespace foodieland.Models;

public class Ingredient
{
    public Guid Id { get; } = Guid.NewGuid(); 
    
    public string Name { get; }

    private Ingredient(string name)
    {
        Name = name;
    }

    public static Ingredient Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name can not be empty", nameof(name));
        
        return new Ingredient(name);
    }
}