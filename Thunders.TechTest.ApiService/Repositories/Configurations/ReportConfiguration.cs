using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thunders.TechTest.ApiService.Models;

namespace Thunders.TechTest.ApiService.Repositories.Configurations
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable("Reports");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.ReportType).IsRequired();
            builder.Property(t => t.GeneratedAt);
            builder.Property(t => t.Data);
            builder.Property(t => t.CreatedBy).IsRequired().HasMaxLength(100);
            builder.Property(t => t.CreatedAt).IsRequired();
        }
    }
}
