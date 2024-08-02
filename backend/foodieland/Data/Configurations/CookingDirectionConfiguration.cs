using foodieland.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace foodieland.Data.Configurations;

public class CookingDirectionConfiguration : IEntityTypeConfiguration<CookingDirectionEntity>
{
    public void Configure(EntityTypeBuilder<CookingDirectionEntity> builder)
    {
        builder.ToTable("CookingDirections");
        builder.HasKey(cd => cd.Id);
    }
}