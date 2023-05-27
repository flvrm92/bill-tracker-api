using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configuration;
public class SubCategoryMap: IEntityTypeConfiguration<SubCategory>
{
  public void Configure(EntityTypeBuilder<SubCategory> builder)
  {
    builder.ToTable("SubCategory");

    builder.HasKey(x => x.Id);

    builder.Property(s => s.Name)
      .IsRequired()
      .HasMaxLength(200);

    builder.HasOne(s => s.Category)
      .WithMany(s => s.SubCategories)
      .HasForeignKey(s => s.CategoryId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
