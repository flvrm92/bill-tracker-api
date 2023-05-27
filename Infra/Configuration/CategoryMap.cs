using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configuration
{
  internal class CategoryMap:IEntityTypeConfiguration<Category>
  {
    public void Configure(EntityTypeBuilder<Category> builder)
    {
      builder.ToTable("Category");

      builder.HasKey(x => x.Id);

      builder.Property(x => x.Name)
        .IsRequired()
        .HasMaxLength(200);
    }
  }
}
