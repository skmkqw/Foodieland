using foodieland.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace foodieland.Data.Configurations;

public class NutritionInformationConfiguration : IEntityTypeConfiguration<NutritionInformation>
{
    public void Configure(EntityTypeBuilder<NutritionInformation> builder)
    {
        builder.HasKey(ni => ni.Id);
    }
}