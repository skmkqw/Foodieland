using foodieland.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace foodieland.Data.Configurations;

public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
        builder.ToTable("Recipes");

        builder.HasKey(r => r.Id);
        
        builder.Property(r => r.Name)
            .IsRequired()
            .IsUnicode()
            .HasMaxLength(100);
        
        builder.Property(r => r.Description)
            .IsRequired()
            .IsUnicode()
            .HasMaxLength(1000);
        
        builder.Property(r => r.CreationDate)
            .HasDefaultValueSql("GETUTCDATE()")
            .ValueGeneratedOnAdd();

        builder.Property(r => r.TimeToCook)
            .IsRequired();

        builder.Property(r => r.Category)
            .IsRequired();


        builder.HasMany(r => r.Ingredients)
            .WithOne(r => r.Recipe)
            .HasForeignKey(ri => ri.RecipeId);

        builder.HasMany(r => r.Directions)
            .WithOne(cd => cd.Recipe)
            .HasForeignKey(cd => cd.RecipeId);

        builder.HasOne(r => r.NutritionInformation)
            .WithOne(ni => ni.Recipe)
            .HasForeignKey<NutritionInformation>(ni => ni.RecipeId);
    }
}