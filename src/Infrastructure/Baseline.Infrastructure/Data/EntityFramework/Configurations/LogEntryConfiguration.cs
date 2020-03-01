using Baseline.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Baseline.Infrastructure.Data.EntityFramework.Configurations
{
    public class LogEntryConfiguration : IEntityTypeConfiguration<LogEntry>
    {
        public void Configure(EntityTypeBuilder<LogEntry> builder)
        {
            builder.Property(t => t.Message)
                .IsRequired();
            
            builder.Property(t => t.MessageTemplate)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(t => t.Level)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}