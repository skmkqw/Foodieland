using foodieland.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace foodieland.Data.Configurations;

public class CookingDirectionConfiguration : IEntityTypeConfiguration<CookingDirection>
{
    public void Configure(EntityTypeBuilder<CookingDirection> builder)
    {
        builder.HasKey(cd => cd.Id);
    }
}