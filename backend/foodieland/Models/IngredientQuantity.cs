using System.Text.Json.Serialization;

namespace foodieland.Models;

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

public class IngredientQuantity
{
    public Guid Id { get; set; }
    public Guid RecipeId { get; init; }
    
    [JsonIgnore]
    public Recipe Recipe { get; set; } = new ();
    
    public Guid IngredientId { get; init; }
    
    [JsonIgnore]
    public Ingredient Ingredient { get; set; } = new ();

    public double Quantity { get; set; }

    public MeasurementUnit Unit { get; set; } = MeasurementUnit.Gram;
}