using foodieland.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace foodieland.Data.Configurations;

public class NutritionInformationConfiguration : IEntityTypeConfiguration<NutritionInformationEntity>
{
    public void Configure(EntityTypeBuilder<NutritionInformationEntity> builder)
    {
        builder.HasKey(ni => ni.Id);
    }
}