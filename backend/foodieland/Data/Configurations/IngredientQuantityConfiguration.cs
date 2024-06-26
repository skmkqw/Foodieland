using foodieland.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace foodieland.Data.Configurations;

public class IngredientQuantityConfiguration : IEntityTypeConfiguration<IngredientQuantity>
{
    public void Configure(EntityTypeBuilder<IngredientQuantity> builder)
    {
        builder.HasKey(ri => ri.Id);
    }
}