using Domain.Entities.Bills;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configuration.Bills
{
  internal class BillItemMap: IEntityTypeConfiguration<BillItem>
  {
    public void Configure(EntityTypeBuilder<BillItem> builder)
    {
      builder.ToTable("BillItems");

      builder.HasKey(x => x.Id);

      builder.Property(s => s.Description)
        .IsRequired()
        .HasColumnType("varchar(200)");

      builder.Property(s => s.Value)
        .IsRequired()
        .HasColumnType("decimal(18,2)");

      builder.HasOne(s => s.Bill)
        .WithMany(s => s.BillItems)
        .HasForeignKey(s => s.BillId)
        .OnDelete(DeleteBehavior.Cascade);

      builder.HasOne(s => s.Category)
        .WithMany(s => s.BillItems)
        .HasForeignKey(s => s.CategoryId)
        .OnDelete(DeleteBehavior.Cascade);
    }
  }
}
