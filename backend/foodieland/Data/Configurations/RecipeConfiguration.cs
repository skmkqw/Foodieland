using foodieland.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace foodieland.Data.Configurations;

public class RecipeConfiguration : IEntityTypeConfiguration<RecipeEntity>
{
    public void Configure(EntityTypeBuilder<RecipeEntity> builder)
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

        builder.Property(r => r.IsPublished)
            .IsRequired();

        builder.HasMany(r => r.Ingredients)
            .WithOne(r => r.RecipeEntity)
            .HasForeignKey(ri => ri.RecipeId);

        builder.HasMany(r => r.Directions)
            .WithOne(cd => cd.RecipeEntity)
            .HasForeignKey(cd => cd.RecipeId);

        builder.HasOne(r => r.NutritionInformation)
            .WithOne(ni => ni.RecipeEntity)
            .HasForeignKey<NutritionInformationEntity>(ni => ni.RecipeId);

        builder.HasOne(r => r.Creator)
            .WithMany(au => au.Recipes)
            .HasForeignKey(r => r.CreatorId);
    }
}