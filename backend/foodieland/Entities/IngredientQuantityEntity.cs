using System.Text.Json.Serialization;
namespace foodieland.Entities;

public enum MeasurementUnit
{
    // Volume
    Teaspoon,
    Tablespoon,
    Cup,
    Milliliter,
    Liter,
    FluidOunce,
    Pint,
    Quart,
    Gallon,

    // Weight
    Gram,
    Kilogram,
    Milligram,
    Ounce,
    Pound,
    Stone,
    Ton,

    // Length
    Millimeter,
    Centimeter,
    Meter,
    Inch,
    Foot,
    

    // Miscellaneous
    Pinch,
    Dash,
    Drop,
    Handful,

    // International units
    Deciliter,
    Centiliter,
    Decagram,
    Hectogram,

    // Cooking specific
    Clove,
    Slice,
    Piece,
    Fillet,
    Stick,
    Can,
    Jar,
    Bottle,
    Packet,
    Bunch,
    Head,
    Leaf,
    Sprig,
    Stalk,
    Strip,

    // Liquid specific
    Barrel,
    Hogshead,
    Gill,
    Firkin,

    // Baking specific
    Sheet,
    Loaf,
    Scoop
}

public class IngredientQuantityEntity
{
    public Guid Id { get; set; }
    public Guid RecipeId { get; init; }
    
    [JsonIgnore]
    public RecipeEntity RecipeEntity { get; set; } = new ();
    
    public Guid IngredientId { get; set; }
    
    [JsonIgnore]
    public IngredientEntity IngredientEntity { get; set; } = new ();

    public double Quantity { get; set; }

    public MeasurementUnit Unit { get; set; } = MeasurementUnit.Gram;
}