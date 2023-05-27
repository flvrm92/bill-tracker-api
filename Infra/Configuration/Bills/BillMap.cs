using Domain.Entities.Bills;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configuration.Bills
{
  internal class BillMap: IEntityTypeConfiguration<Bill>
  {
    public void Configure(EntityTypeBuilder<Bill> builder)
    {
      builder.ToTable("Bill");

      builder.HasKey(x => x.Id);

      builder.Property(x => x.UserId)
        .IsRequired();

      builder.Property(x => x.Payment)
        .IsRequired();

      builder.Property(x => x.Total)
        .IsRequired()
        .HasColumnType("decimal(18,2)");

      builder.Property(x => x.TotalIncoming)
        .HasColumnType("decimal(18,2)");
    }
  }
}
