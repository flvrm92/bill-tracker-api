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

      builder.Property(x => x.PaymentMonth)
        .HasColumnType("date")
        .IsRequired();

      builder.Property(x => x.TotalIncoming)
        .HasColumnType("decimal(18,2)");
    }
  }
}
