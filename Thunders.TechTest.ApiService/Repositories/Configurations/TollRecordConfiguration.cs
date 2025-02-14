using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thunders.TechTest.ApiService.Models;

namespace Thunders.TechTest.ApiService.Repositories.Configurations
{
    public class TollRecordConfiguration : IEntityTypeConfiguration<TollRecord>
    {
        public void Configure(EntityTypeBuilder<TollRecord> builder)
        {
            builder.ToTable("TollRecords");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Timestamp).IsRequired();
            builder.Property(t => t.TollBooth).IsRequired().HasMaxLength(100);
            builder.Property(t => t.City).IsRequired().HasMaxLength(100);
            builder.Property(t => t.State).IsRequired().HasMaxLength(100);
            builder.Property(t => t.AmountPaid).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(t => t.VehicleType).IsRequired();
            builder.Property(t => t.LicensePlate).IsRequired().HasMaxLength(100);
            builder.Property(t => t.TransactionId).HasMaxLength(100);
            builder.Property(t => t.CreatedBy).IsRequired().HasMaxLength(100);
            builder.Property(t => t.CreatedAt).IsRequired();
            builder.Property(t => t.IsDeleted).IsRequired();
            builder.Property(t => t.PreviousRecordId);
            builder.Property(t => t.DeletedBy).HasMaxLength(100);
            builder.Property(t => t.DeletedAt);
        }
    }
}
